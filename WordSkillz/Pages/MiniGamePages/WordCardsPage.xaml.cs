using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Compatibility;
using Plugin.TextToSpeech;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WordSkillz.Models;
using WordSkillz.Popup;
using WordSkillz.Tools;

namespace WordSkillz.Pages.MiniGamePages;

public partial class WordCardsPage : ContentPage
{
    private int currentIndex = 0;
    private int allCountWords;
    private int currentWordCount;
    private bool isTimerRunning = false;
    private Category contextCategory;
    public List<Word> Words { get; set; }
    private CancellationTokenSource cancellationTokenSource;

    [Obsolete]
    public WordCardsPage(Category category, List<Word> words)
    {
        InitializeComponent();
        contextCategory = category;
        Words = words;
        allCountWords = Words.Count;
        currentWordCount = 0;
        BindingContext = this;
        LVWord�ards.ItemsSource = Words.Take(1);
        Refresh();
        UpdateProgress();
    }

    [Obsolete]
    private async void Refresh()
    {
        if (cancellationTokenSource != null)
            cancellationTokenSource.Cancel();
        cancellationTokenSource = new CancellationTokenSource();
        await TextToSpeech(cancellationTokenSource.Token);
    }

    [Obsolete]
    private async void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        try
        {
            await LVWord�ards.FadeTo(0, 250);

            if (e.SwipeDirection == SwipeDirection.Left)
            {
                Words.RemoveAt(currentIndex);
                App.loggedUser.LearnedWordsCount += 1;
                if (currentIndex >= Words.Count)
                {
                    // ���� ������� ������ ������� �� ������� ����������� ���������, ��������� ���
                    currentIndex = Words.Count > 0 ? 0 : -1;
                    if (currentIndex == -1)
                    {
                        uint animationLength = 1000;
                        WordsLeftLabel.Text = allCountWords.ToString();
                        await ProgressBar.ProgressTo(allCountWords, animationLength, Easing.Linear);
                        // ��� ����� ���� ��������� ������, ���������� ���������
                        //await DisplayAlert("Congratulate", "You've looked at all the words!", "OK");
                        var congratulatePopup = new CongratulatePopup();
                        var popup = new CommunityToolkit.Maui.Views.Popup();
                        popup.Content = congratulatePopup;
                        popup.Color = Color.FromRgba(0, 0, 0, 0);
                        // �������� � �������� TaskCompletionSource
                        var popupClosedTask = new TaskCompletionSource<object>();

                        // ���������� ������� Closed ��� ���������� TaskCompletionSource
                        popup.Closed += (sender, args) =>
                        {
                            popupClosedTask.SetResult(null);
                        };

                        App.Current.MainPage.ShowPopup(popup);
                        // �������� ���������� ������������ ����

                        await popupClosedTask.Task;

                        // ����� ��������� Words � ��������� ������
                        var wordsInDB = await NetManager.Get<List<Word>>("api/Words");
                        Words = wordsInDB.Where(x => x.CategoryId == contextCategory.Id).ToList();
                        // �������� LVWord�ards.ItemsSource
                        LVWord�ards.ItemsSource = Words;
                        currentIndex = 0;
                    }
                }
            }
            currentWordCount = allCountWords - Words.Count;
            LVWord�ards.ItemsSource = Words.Skip(currentIndex).Take(1);
            await LVWord�ards.FadeTo(1, 250);
            NetManager.Put($"api/Users/{App.loggedUser.Id}", ToUserDTO(App.loggedUser));

            UpdateProgress();
            Refresh();
        }
        catch (Exception ex)
        {
            return;
        }
    }
    private UserDTO ToUserDTO(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Image = user.Image,
            PhoneNumber = user.PhoneNumber,
            LearnedWordsCount = user.LearnedWordsCount,
            IncorrectAnswersCount = user.IncorrectAnswersCount,
            CorrectAnswersCount = user.CorrectAnswersCount
        };
    }
    private async void UpdateProgress()
    {
        // ���������� ���������
        int wordsLeft = currentWordCount;
        WordsLeftLabel.Text = wordsLeft.ToString();
        TotalWordsLabel.Text = allCountWords.ToString();
        double progress = (double)currentWordCount / allCountWords;

        // �������� �������� ��� ���������� ��������-����
        uint animationLength = 1000;
        double startProgress = ProgressBar.Progress;
        double endProgress = progress;

        await ProgressBar.ProgressTo(endProgress, animationLength, Easing.Linear);
    }

    [Obsolete]
    private async Task TextToSpeech(CancellationToken cancellationToken)
    {
        var currentWord = Words[currentIndex];
        try
        {
            // ��������������� ������ � ���� ����
            if (Device.RuntimePlatform == Device.Android)
            {
                var speakOriginal = CrossTextToSpeech.Current.Speak(currentWord.OriginalWord, null, null, 1.0f, null, cancellationToken);
                var speakTranslated = CrossTextToSpeech.Current.Speak(currentWord.TranslatedWord, null, null, 1.0f, null, cancellationToken);
                // ������� ������ �����������
                await Task.WhenAll(speakOriginal, speakTranslated);
            }

        }
        catch (OperationCanceledException)
        {

        }
    }
    private void ContentPage_Disappearing(object sender, EventArgs e)
    {
        try
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void SwipeView_SwipeChanging(object sender, SwipeChangingEventArgs e)
    {

    }

    private void LVWord�ards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        LVWord�ards.SelectedItem = null;
    }
}

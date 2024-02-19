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
    public ObservableCollection<Word> Words { get; set; }
    private CancellationTokenSource cancellationTokenSource;


    public WordCardsPage(Category category)
    {
        InitializeComponent();
        contextCategory = category;
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == category.Id));
        allCountWords = Words.Count;
        currentWordCount = 0;
        BindingContext = this;
        // ������������� ��������� �������� ������, ������� ������ ���� �������
        LVWord�ards.ItemsSource = Words.Take(1);
        Refresh();
        // ���������� ���������
        UpdateProgress();
    }
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
                if (currentIndex >= Words.Count)
                {
                    // ���� ������� ������ ������� �� ������� ����������� ���������, ��������� ���
                    currentIndex = Words.Count > 0 ? 0 : -1;
                    if (currentIndex == -1)
                    {
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
                        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id));
                        // �������� LVWord�ards.ItemsSource
                        LVWord�ards.ItemsSource = Words;
                        currentIndex = 0;
                    }
                }
            }
            else if (e.SwipeDirection == SwipeDirection.Right)
            {
                currentIndex++;
                if (currentIndex >= Words.Count)
                {
                    currentIndex = 0;
                }
            }
            currentWordCount = allCountWords - Words.Count;
            LVWord�ards.ItemsSource = Words.Skip(currentIndex).Take(1);
            await LVWord�ards.FadeTo(1, 250);
            UpdateProgress();
            Refresh();
        }
        catch (Exception ex)
        {

        }
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
        uint steps = 100;
        double startProgress = ProgressBar.Progress;
        double endProgress = progress;

        await ProgressBar.ProgressTo(endProgress, animationLength, Easing.Linear);

        // ����� ����� �������� �������������� �������� ����� ���������� ��������, ���� ����������
    }
    private async Task TextToSpeech(CancellationToken cancellationToken)
    {
        var currentWord = Words[currentIndex];
        try
        {
            // ��������������� ������ � ���� ����
            if(Device.RuntimePlatform == Device.Android) 
            {
                var speakOriginal = CrossTextToSpeech.Current.Speak(currentWord.OriginalWord, null, null, 1.0f, null, cancellationToken);
                //var speakTranslated = CrossTextToSpeech.Current.Speak(currentWord.TranslatedWord, null, null, 1.0f, null, cancellationToken);
                // ������� ������ �����������
                //await Task.WhenAll(speakOriginal, speakTranslated);
                await Task.WhenAll(speakOriginal);
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
    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        SKSurface surface = e.Surface;
        SKCanvas canvas = surface.Canvas;

        canvas.Clear();

        using (SKPaint paint = new SKPaint())
        {
            // ������������� ���� ���� � ������
            paint.Color = SKColors.Transparent;

            // ������ ������������� � ������� ������
            canvas.DrawRect(new SKRect(0, 0, e.Info.Width, e.Info.Height), paint);

            // ������������� ���� ������
            // C����� ����� � ������� HEX
            string hexColor = string.Empty;
            if (Application.Current.UserAppTheme == AppTheme.Light)
                hexColor = "#0875BA";
            else
                hexColor = "#203E5F";

            // �������������� HEX ������ � .NET Color
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(hexColor);

            // �������� SKColor �� .NET Color
            SKColor skColor = new SKColor(color.R, color.G, color.B, color.A);
            
            paint.Color = skColor;
            // ������������� ������ � ����� ������
            paint.TextSize = 100;
            paint.IsAntialias = true;

            // ������� ������ ��������
            var blurFilter = SKImageFilter.CreateBlur(15, 15);

            // ��������� ������ � ������
            paint.ImageFilter = blurFilter;

            // ������ �����
            canvas.DrawText("Blurred", 170, 270, paint);
        }
    }


}

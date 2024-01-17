using Microsoft.Maui.Controls.Compatibility;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class WordCardsPage : ContentPage
{
    private int currentIndex = 0;
    public ObservableCollection<Word> Words { get; set; }
    private bool isTimerRunning = false;
    public WordCardsPage(Category category)
    {
        InitializeComponent();
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == category.Id));
        BindingContext = this;
        // ������������� ��������� �������� ������, ������� ������ ���� �������
        LVWord�ards.ItemsSource = Words.Take(1);
        if (Words.Count != 0)
            TextToSpeech();
    }
    private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        SwipeToNextCard();
    }
    private async void SwipeToNextCard()
    {
        await LVWord�ards.FadeTo(0, 250);

        currentIndex++;
        if (currentIndex >= Words.Count)
        {
            currentIndex = 0;
        }

        LVWord�ards.ItemsSource = Words.Skip(currentIndex).Take(1);
        await LVWord�ards.FadeTo(1, 250);
        TextToSpeech();
    }
    private async void TextToSpeech()
    {
        var currentWord = Words[currentIndex];
        // ��������������� ������ � ���� ����
        await CrossTextToSpeech.Current.Speak(currentWord.OriginalWord);
        await CrossTextToSpeech.Current.Speak(currentWord.TranslatedWord);
    }
    [Obsolete]
    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        StartTimer();
    }

    private void ContentPage_Disappearing(object sender, EventArgs e)
    {
        StopTimer();
    }

    [Obsolete]
    private void StartTimer()
    {
        isTimerRunning = true;
        Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        {
            if (isTimerRunning)
            {
                SwipeToNextCard();
                return true;
            }
            return false; // ������ ����������
        });
    }
    private void StopTimer()
    {
        isTimerRunning = false;
    }
    private void SwipeView_SwipeChanging(object sender, SwipeChangingEventArgs e)
    {

    }
}

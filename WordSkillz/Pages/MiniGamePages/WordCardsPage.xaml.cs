using Microsoft.Maui.Controls.Compatibility;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages.MiniGamePages;

public partial class WordCardsPage : ContentPage
{
    private int currentIndex = 0;
    private bool isTimerRunning = false;
    public ObservableCollection<Word> Words { get; set; }
    private CancellationTokenSource cancellationTokenSource;

    public WordCardsPage(Category category)
    {
        InitializeComponent();
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == category.Id));
        BindingContext = this;
        // Устанавливаем начальный источник данных, включая только один элемент
        LVWordСards.ItemsSource = Words.Take(1);
        Refresh();
    }
    private async void Refresh()
    {
        if (cancellationTokenSource != null)
            cancellationTokenSource.Cancel();
        cancellationTokenSource = new CancellationTokenSource();
        await TextToSpeech(cancellationTokenSource.Token);
    }

    [Obsolete]
    private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        SwipeToNextCard();
    }
    private async void SwipeToNextCard()
    {
        await LVWordСards.FadeTo(0, 250);

        currentIndex++;
        if (currentIndex >= Words.Count)
        {
            currentIndex = 0;
            StopTimer();
        }

        LVWordСards.ItemsSource = Words.Skip(currentIndex).Take(1);
        await LVWordСards.FadeTo(1, 250);
        Refresh();
    }
    private async Task TextToSpeech(CancellationToken cancellationToken)
    {
        var currentWord = Words[currentIndex];
        try
        {
            // Воспроизведение текста в виде речи
            var speakOriginal = CrossTextToSpeech.Current.Speak(currentWord.OriginalWord, null, null, 1.0f, null, cancellationToken);
            var speakTranslated = CrossTextToSpeech.Current.Speak(currentWord.TranslatedWord, null, null, 1.0f, null, cancellationToken);
            // Возврат задачи озвучивания
            await Task.WhenAll(speakOriginal, speakTranslated);
        }
        catch (OperationCanceledException)
        {
            
        }
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
            return false; // Таймер остановлен
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

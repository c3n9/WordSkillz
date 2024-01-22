using Microsoft.Maui.Controls.Compatibility;
using Plugin.TextToSpeech;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages.MiniGamePages;

public partial class WordCardsPage : ContentPage
{
    private int currentIndex = 0;
    private bool isTimerRunning = false;
    private Category contextCategory;
    public ObservableCollection<Word> Words { get; set; }
    private CancellationTokenSource cancellationTokenSource;

    public WordCardsPage(Category category)
    {
        InitializeComponent();
        contextCategory = category;
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
    private async void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        await LVWordСards.FadeTo(0, 250);

        if (e.SwipeDirection == SwipeDirection.Left)
        {
            Words.RemoveAt(currentIndex);
            if (currentIndex >= Words.Count)
            {
                // Если текущий индекс выходит за пределы обновленной коллекции, уменьшите его
                currentIndex = Words.Count > 0 ? 0 : -1;
                if (currentIndex == -1)
                {
                    // Все слова были свайпнуты вправо, отображаем сообщение
                    await DisplayAlert("Поздравляем", "Вы просмотрели все слова!", "OK");
                    // Сброс коллекции Words к исходному набору
                    Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id));
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
    private void ContentPage_Disappearing(object sender, EventArgs e)
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }
    }
    private void SwipeView_SwipeChanging(object sender, SwipeChangingEventArgs e)
    {

    }

    
}

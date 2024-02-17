using Microsoft.Maui.Controls.Compatibility;
using Plugin.TextToSpeech;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WordSkillz.Models;
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
        // Устанавливаем начальный источник данных, включая только один элемент
        LVWordСards.ItemsSource = Words.Take(1);
        Refresh();
        // Обновление прогресса
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
                        await DisplayAlert("Congratulate", "You've looked at all the words!", "OK");
                        // Сброс коллекции Words к исходному набору
                        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id));
                        // Обновите LVWordСards.ItemsSource
                        LVWordСards.ItemsSource = Words;

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
            LVWordСards.ItemsSource = Words.Skip(currentIndex).Take(1);
            await LVWordСards.FadeTo(1, 250);
            UpdateProgress();
            Refresh();
        }
        catch (Exception ex)
        {

        }
    }
    private async void UpdateProgress()
    {
        // Обновление прогресса
        int wordsLeft = currentWordCount;
        WordsLeftLabel.Text = wordsLeft.ToString();
        TotalWordsLabel.Text = allCountWords.ToString();
        double progress = (double)currentWordCount / allCountWords;

        // Создание анимации для заполнения прогресс-бара
        uint animationLength = 1000;
        uint steps = 100;
        double startProgress = ProgressBar.Progress;
        double endProgress = progress;

        await ProgressBar.ProgressTo(endProgress, animationLength, Easing.Linear);

        // Здесь можно добавить дополнительные действия после завершения анимации, если необходимо
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
            // Устанавливаем цвет фона и текста
            paint.Color = SKColors.Transparent;

            // Рисуем прямоугольник в размере холста
            canvas.DrawRect(new SKRect(0, 0, e.Info.Width, e.Info.Height), paint);

            // Устанавливаем цвет текста
            // Cтрока цвета в формате HEX
            string hexColor = string.Empty;
            if (Application.Current.UserAppTheme == AppTheme.Light)
                hexColor = "#0875BA";
            else
                hexColor = "#203E5F";

            // Преобразование HEX строки в .NET Color
            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(hexColor);

            // Создание SKColor из .NET Color
            SKColor skColor = new SKColor(color.R, color.G, color.B, color.A);
            
            paint.Color = skColor;
            // Устанавливаем размер и стиль шрифта
            paint.TextSize = 100;
            paint.IsAntialias = true;

            // Создаем фильтр размытия
            var blurFilter = SKImageFilter.CreateBlur(15, 15);

            // Применяем фильтр к тексту
            paint.ImageFilter = blurFilter;

            // Рисуем текст
            canvas.DrawText("Blurred", 170, 270, paint);
        }
    }


}

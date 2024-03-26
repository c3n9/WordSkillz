using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Popup;
using WordSkillz.Tools;

namespace WordSkillz.Pages.MiniGamePages;

public partial class MatchWordsCard : ContentPage
{
    private int currentIndex = 0;
    SQLiteDbContext db;
    private int allCountWords;
    private int currentWordCount;
    private Word currentWord;
    private int correctAnswerIndex;
    private Category contextCategory;
    public ObservableCollection<Word> Words { get; set; }
    public ObservableCollection<Word> WordsToShuffle { get; set; }

    public MatchWordsCard(Category category, List<Word> words)
    {
        InitializeComponent();
        db = new SQLiteDbContext();
        contextCategory = category;
        Words = new ObservableCollection<Word>(words);
        WordsToShuffle = new ObservableCollection<Word>(words);
        allCountWords = Words.Count;
        currentWordCount = 0;
        Refresh();
        UpdateProgress();
    }

    private async void Refresh()
    {
        currentWordCount = allCountWords - Words.Count;
        currentWord = Words[currentIndex];
        await FadeInNewWord();
        await ShuffleAndSetButtonTexts();
    }
    private async void UpdateProgress()
    {
        int wordsLeft = currentWordCount;
        WordsLeftLabel.Text = wordsLeft.ToString();
        TotalWordsLabel.Text = allCountWords.ToString();
        double progress = (double)currentWordCount / allCountWords;
        uint animationLength = 1000;
        double startProgress = ProgressBar.Progress;
        double endProgress = progress;
        await ProgressBar.ProgressTo(endProgress, animationLength, Easing.Linear);
    }

    private async Task FadeInNewWord()
    {
        await TBWord.FadeTo(0, 250);
        TBWord.Text = currentWord.OriginalWord;
        await TBWord.FadeTo(1, 250);
    }


    private async Task ShuffleAndSetButtonTexts()
    {
        // Блокируем кнопки, чтобы предотвратить их нажатие во время анимации
        BWord1.IsEnabled = BWord2.IsEnabled = BWord3.IsEnabled = BWord4.IsEnabled = false;
        var word1 = string.Empty;
        var word2 = string.Empty;
        var word3 = string.Empty;
        var word4 = string.Empty;
        int correctTranslationIndex = new Random().Next(4);
        correctAnswerIndex = correctTranslationIndex;

        // Проверяем, достаточно ли слов для заполнения всех кнопок
        if (WordsToShuffle.Count >= 4)
        {
            switch (correctTranslationIndex)
            {
                case 0:
                    word1 = currentWord.TranslatedWord;
                    break;
                case 1:
                    word2 = currentWord.TranslatedWord;
                    break;
                case 2:
                    word3 = currentWord.TranslatedWord;
                    break;
                case 3:
                    word4 = currentWord.TranslatedWord;
                    break;
            }

            // Получаем все неправильные переводы, исключая правильный
            var incorrectTranslations = WordsToShuffle.Where(word => word != currentWord)
                                                      .Select(word => word.TranslatedWord)
                                                      .ToList();

            // Перемешиваем неправильные переводы
            incorrectTranslations = incorrectTranslations.OrderBy(x => Guid.NewGuid()).ToList();

            // Анимация появления неправильных переводов
            int buttonIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                if (i != correctTranslationIndex)
                {
                    switch (i)
                    {
                        case 0:
                            word1 = incorrectTranslations[buttonIndex];
                            break;
                        case 1:
                            word2 = incorrectTranslations[buttonIndex];
                            break;
                        case 2:
                            word3 = incorrectTranslations[buttonIndex];
                            break;
                        case 3:
                            word4 = incorrectTranslations[buttonIndex];
                            break;
                    }
                    buttonIndex++;
                }
            }
        }
        else
        {
            // Заполняем кнопки словами по кругу (циклически)
            for (int i = 0; i < 4; i++)
            {
                int wordIndex = i % WordsToShuffle.Count;
                switch (i)
                {
                    case 0:
                        word1 = WordsToShuffle[wordIndex].TranslatedWord;
                        break;
                    case 1:
                        word2 = WordsToShuffle[wordIndex].TranslatedWord;
                        break;
                    case 2:
                        word3 = WordsToShuffle[wordIndex].TranslatedWord;
                        break;
                    case 3:
                        word4 = WordsToShuffle[wordIndex].TranslatedWord;
                        break;
                }
            }
        }

        BWord1.Text = word1;
        BWord1.Opacity = 10;
        await AnimateButtonTextAppearance(BWord1);
        BWord2.Text = word2;
        BWord2.Opacity = 10;
        await AnimateButtonTextAppearance(BWord2);
        BWord3.Text = word3;
        BWord3.Opacity = 10;
        await AnimateButtonTextAppearance(BWord3);
        BWord4.Text = word4;
        BWord4.Opacity = 10;
        await AnimateButtonTextAppearance(BWord4);


        // Разблокируем кнопки после завершения анимации
        BWord1.IsEnabled = BWord2.IsEnabled = BWord3.IsEnabled = BWord4.IsEnabled = true;
        BWord1.BackgroundColor = BWord2.BackgroundColor = BWord2.BackgroundColor = BWord3.BackgroundColor = BWord4.BackgroundColor = Color.FromHex("#0875BA");

    }


    private async Task AnimateButtonTextAppearance(Button button)
    {
        // Устанавливаем начальную прозрачность текста
        button.Opacity = 0;

        // Анимация появления текста с плавным эффектом прозрачности
        await Task.WhenAll(
            button.FadeTo(1, 500, Easing.CubicInOut), // Анимация появления текста
            button.TranslateTo(0, -10, 250, Easing.CubicInOut) // Анимация подъема текста
        );
    }
    [Obsolete]
    private async Task CheckAnswer(Button button)
    {
        var isCorrect = false;
        if (button.Text == currentWord.TranslatedWord)
            isCorrect = true;
        else
            isCorrect = false;
        if (isCorrect)
        {
            currentWordCount++;
            Words.RemoveAt(currentIndex);
            if (currentIndex >= Words.Count)
            {
                currentIndex = Words.Count > 0 ? 0 : -1;
                if (currentIndex == -1)
                {
                    button.BackgroundColor = Color.FromHex("#228b22");

                    var congratulatePopup = new CongratulatePopup();
                    var popup = new CommunityToolkit.Maui.Views.Popup();
                    popup.Content = congratulatePopup;
                    popup.Color = Color.FromRgba(0, 0, 0, 0);
                    var popupClosedTask = new TaskCompletionSource<object>();
                    popup.Closed += (sender, args) =>
                    {
                        popupClosedTask.SetResult(null);
                    };
                    App.Current.MainPage.ShowPopup(popup);
                    await popupClosedTask.Task;

                    Words = new ObservableCollection<Word>((await db.GetAllWord()).Where(x => x.CategoryId == contextCategory.Id).ToList());
                    allCountWords = Words.Count;
                    currentIndex = 0;
                    currentWordCount = 0;
                    UpdateProgress();
                    Refresh();
                    await Task.Delay(100);
                    button.BackgroundColor = Color.FromHex("#0875BA");

                    return;
                }
            }
            button.BackgroundColor = Color.FromHex("#228b22");
            await Task.Delay(1000);
            button.BackgroundColor = Color.FromHex("#0875BA");
            UpdateProgress();
        }
        else
        {
            currentIndex++;
            if (currentIndex >= Words.Count)
            {
                currentIndex = 0;
            }
            button.BackgroundColor = Color.FromRgb(255, 0, 0);
            await Task.Delay(1000);
            switch (correctAnswerIndex)
            {
                case 0:
                    BWord1.BackgroundColor = Color.FromHex("#228b22");
                    await Task.Delay(1000);
                    break;
                case 1:
                    BWord2.BackgroundColor = Color.FromHex("#228b22");
                    await Task.Delay(1000);
                    break;
                case 2:
                    BWord3.BackgroundColor = Color.FromHex("#228b22");
                    await Task.Delay(1000);
                    break;
                case 3:
                    BWord4.BackgroundColor = Color.FromHex("#228b22");
                    await Task.Delay(1000);
                    break;
            }
        }

        currentWord = Words[currentIndex];
        BWord1.BackgroundColor = BWord2.BackgroundColor = BWord2.BackgroundColor = BWord3.BackgroundColor = BWord4.BackgroundColor = Color.FromHex("#0875ba");

        await FadeInNewWord();
        await ShuffleAndSetButtonTexts();

        UpdateProgress();
    }






    [Obsolete]
    private void BWord1_Clicked(object sender, EventArgs e)
    {
        BWord1.IsEnabled = BWord2.IsEnabled = BWord3.IsEnabled = BWord4.IsEnabled = false;
        CheckAnswer((Button)sender);
    }

    [Obsolete]
    private void BWord2_Clicked(object sender, EventArgs e)
    {
        BWord1.IsEnabled = BWord2.IsEnabled = BWord3.IsEnabled = BWord4.IsEnabled = false;
        CheckAnswer((Button)sender);
    }

    [Obsolete]
    private void BWord3_Clicked(object sender, EventArgs e)
    {
        BWord1.IsEnabled = BWord2.IsEnabled = BWord3.IsEnabled = BWord4.IsEnabled = false;
        CheckAnswer((Button)sender);
    }

    [Obsolete]
    private void BWord4_Clicked(object sender, EventArgs e)
    {
        BWord1.IsEnabled = BWord2.IsEnabled = BWord3.IsEnabled = BWord4.IsEnabled = false;
        CheckAnswer((Button)sender);
    }
}
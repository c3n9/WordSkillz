using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages.MiniGamePages;

public partial class MatchWordsCard : ContentPage
{
    private int currentIndex = 0;
    private int allCountWords;
    private int currentWordCount;
    private Word currentWord;
    private Category contextCategory;
    public ObservableCollection<Word> Words { get; set; }

    public MatchWordsCard(Category category)
    {
        InitializeComponent();
        contextCategory = category;
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == category.Id));
        allCountWords = Words.Count;
        currentWordCount = 0;
        UpdateProgress();
        LoadNextWord();
    }

    private async void UpdateProgress()
    {
        int wordsLeft = allCountWords - currentWordCount;
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

    private async Task FadeOutButtons()
    {
        await Task.WhenAll(
            BWord1.FadeTo(0, 250),
            BWord2.FadeTo(0, 250),
            BWord3.FadeTo(0, 250),
            BWord4.FadeTo(0, 250)
        );
    }

    private async Task FadeInButtons()
    {
        await Task.WhenAll(
            BWord1.FadeTo(1, 250),
            BWord2.FadeTo(1, 250),
            BWord3.FadeTo(1, 250),
            BWord4.FadeTo(1, 250)
        );
    }

    private async Task ShuffleAndSetButtonTexts()
    {
        // Генерируем случайный индекс для правильного перевода
        int correctTranslationIndex = new Random().Next(4);

        // Устанавливаем правильный перевод в случайную кнопку
        switch (correctTranslationIndex)
        {
            case 0:
                BWord1.Text = currentWord.TranslatedWord;
                break;
            case 1:
                BWord2.Text = currentWord.TranslatedWord;
                break;
            case 2:
                BWord3.Text = currentWord.TranslatedWord;
                break;
            case 3:
                BWord4.Text = currentWord.TranslatedWord;
                break;
        }

        // Получаем все неправильные переводы, исключая правильный
        var incorrectTranslations = Words.Where(word => word != currentWord)
                                          .Select(word => word.TranslatedWord)
                                          .ToList();

        // Перемешиваем неправильные переводы
        incorrectTranslations = incorrectTranslations.OrderBy(x => Guid.NewGuid()).ToList();

        // Распределяем неправильные переводы по оставшимся кнопкам
        int buttonIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (i != correctTranslationIndex)
            {
                switch (i)
                {
                    case 0:
                        BWord1.Text = incorrectTranslations[buttonIndex];
                        break;
                    case 1:
                        BWord2.Text = incorrectTranslations[buttonIndex];
                        break;
                    case 2:
                        BWord3.Text = incorrectTranslations[buttonIndex];
                        break;
                    case 3:
                        BWord4.Text = incorrectTranslations[buttonIndex];
                        break;
                }
                buttonIndex++;
            }
        }
    }



    private async Task LoadNextWord()
    {
        currentIndex++;
        if (currentIndex < allCountWords)
        {
            currentWord = Words[currentIndex];
            await FadeInNewWord();
            await ShuffleAndSetButtonTexts();
        }
        else
        {
            currentIndex = 0;
            currentWordCount = 0;
            UpdateProgress();
            await LoadNextWord();
        }
    }

    [Obsolete]
    private async void CheckAnswer(Button button)
    {
        if (button.Text == currentWord.TranslatedWord)
        {
            currentWordCount++;
            UpdateProgress();
            button.BackgroundColor = Color.FromHex("#228b22"); // Подсветка зеленым, если ответ верный
        }
        else
        {
            button.BackgroundColor = Color.FromRgb(255, 0, 0); // Подсветка красным, если ответ неверный
        }

        await Task.Delay(1000); // Задержка для визуальной обратной связи
        button.BackgroundColor = Color.FromHex("#0875BA"); // Возвращаем стандартный фон кнопки

        LoadNextWord();
    }


    private void BWord1_Clicked(object sender, EventArgs e)
    {
        CheckAnswer((Button)sender);
    }

    private void BWord2_Clicked(object sender, EventArgs e)
    {
        CheckAnswer((Button)sender);
    }

    private void BWord3_Clicked(object sender, EventArgs e)
    {
        CheckAnswer((Button)sender);
    }

    private void BWord4_Clicked(object sender, EventArgs e)
    {
        CheckAnswer((Button)sender);
    }
}
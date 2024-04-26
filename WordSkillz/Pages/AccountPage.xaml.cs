using Microcharts;
using SkiaSharp;

namespace WordSkillz.Pages;

public partial class AccountPage : ContentPage
{
    public AccountPage()
    {
        InitializeComponent();
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateText();
        InitChart();
    }

    private void InitChart()
    {
        ChartView.Chart = new DonutChart()
        {
            BackgroundColor = SKColor.Empty,
            LabelTextSize = 50,
            Entries = new List<ChartEntry>()
            {
                new ChartEntry(App.LearnedWordsCount)
                {
                    ValueLabel = App.LearnedWordsCount.ToString(),
                    Color=SKColor.Parse("#91b4bf")
                },
                new ChartEntry(App.IncorrectAnswersCount)
                {
                    ValueLabel = App.IncorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#65869e")
                },
                new ChartEntry(App.CorrectAnswersCount)
                {
                    ValueLabel = App.CorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#cadbec")
                },
            }
        };
    }

    private void UpdateText()
    {
        TBLearned.Text = $"Выучено слов: {App.LearnedWordsCount}";
        TBCorrect.Text = $"Верных ответов: {App.CorrectAnswersCount}";
        TBIncorrect.Text = $"Неверных ответов: {App.IncorrectAnswersCount}";
    }
}
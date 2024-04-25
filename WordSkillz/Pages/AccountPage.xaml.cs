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
            Entries = new List<ChartEntry>()
            {
                new ChartEntry(App.LearnedWordsCount)
                {
                    ValueLabel = App.LearnedWordsCount.ToString(),
                    Color=SKColor.Parse("#42aaff")
                },
                new ChartEntry(App.IncorrectAnswersCount)
                {
                    ValueLabel = App.IncorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#ff3333")
                },
                new ChartEntry(App.CorrectAnswersCount)
                {
                    ValueLabel = App.CorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#00e600")
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
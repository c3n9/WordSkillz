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
                    Color=SKColor.Parse("#92B5C1")
                },
                new ChartEntry(App.IncorrectAnswersCount)
                {
                    ValueLabel = App.IncorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#6587A1")
                },
                new ChartEntry(App.CorrectAnswersCount)
                {
                    ValueLabel = App.CorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#CBDCED")
                },
            }
        };
    }

    private void UpdateText()
    {
        TBLearned.Text = $"������� ����: {App.LearnedWordsCount}";
        TBCorrect.Text = $"������ �������: {App.CorrectAnswersCount}";
        TBIncorrect.Text = $"�������� �������: {App.IncorrectAnswersCount}";
    }
}
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

namespace WordSkillz.Pages;

public partial class AccountPage : ContentPage
{
    public ISeries[] Series { get; set; }
    public AccountPage()
    {
        InitializeComponent();
        Series = new ISeries[]
        {
            new PieSeries<double> {Values = new double[] { App.LearnedWordsCount} },
            new PieSeries<double> {Values = new double[] { App.CorrectAnswersCount} },
            new PieSeries<double> {Values = new double[] { App.IncorrectAnswersCount} },
        };
        MyPieChart.Series = Series;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateText();
    }

    private void UpdateText()
    {
        TBLearned.Text = $"Выучено слов: {App.LearnedWordsCount}";
        TBCorrect.Text = $"Верных ответов: {App.CorrectAnswersCount}";
        TBIncorrect.Text = $"Неверных ответов: {App.IncorrectAnswersCount}";
    }
}
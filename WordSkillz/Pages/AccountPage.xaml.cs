using Microcharts;
using SkiaSharp;
using WordSkillz.Models;

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
        BindingContext = null;
        BindingContext = App.loggedUser;
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
                new ChartEntry(App.loggedUser.LearnedWordsCount)
                {
                    ValueLabel = App.loggedUser.LearnedWordsCount.ToString(),
                    Color=SKColor.Parse("#91b4bf")
                },
                new ChartEntry(App.loggedUser.IncorrectAnswersCount)
                {
                    ValueLabel = App.loggedUser.IncorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#65869e")
                },
                new ChartEntry(App.loggedUser.CorrectAnswersCount)
                {
                    ValueLabel = App.loggedUser.CorrectAnswersCount.ToString(),
                    Color=SKColor.Parse("#cadbec")
                },
            }
        };
    }

    private void TBIExit_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("userId", 0);
        App.loggedUser = null;
        App.Current.MainPage = new LoginPage();
    }

    private void TBIEdit_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistrationPage(App.loggedUser));
    }
}
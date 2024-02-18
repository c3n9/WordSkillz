using WordSkillz.Models;
using WordSkillz.Pages.MiniGamePages;

namespace WordSkillz.Popup;

public partial class MiniGamesPopup : ContentView
{
	public Category Category { get; set; }
	public MiniGamesPopup()
	{
		InitializeComponent();
	}

    private async void BPlayViewWords_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
        await App.Current.MainPage.Navigation.PushAsync(new WordCardsPage(Category));
    }

    private void BPlayMatchWords_Clicked(object sender, EventArgs e)
    {

    }
}
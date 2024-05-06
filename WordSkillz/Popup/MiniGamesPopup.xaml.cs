using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Pages.MiniGamePages;
using WordSkillz.Tools;

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
        try
        {
            if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
            {
                parentPopup.Close();
            }
            var wordsInDB = await NetManager.Get<List<Word>>("api/Words");
            var words = wordsInDB.Where(x => x.CategoryId == Category.Id).ToList();
            if (words.Count != 0)
                await App.Current.MainPage.Navigation.PushAsync(new WordCardsPage(Category, words));

        }
        catch
        {
            return;

        }
    }

    private async void BPlayMatchWords_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
            {
                parentPopup.Close();
            }
            var wordsInDB = await NetManager.Get<List<Word>>("api/Words");
            var words = wordsInDB.Where(x => x.CategoryId == Category.Id).ToList();
            if (words.Count != 0)
                await App.Current.MainPage.Navigation.PushAsync(new MatchWordsCard(Category, words));

        }
        catch
        {
            return;
        }
    }

    private async void BPlayHiddenWords_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
            {
                parentPopup.Close();
            }
            var wordsInDB = await NetManager.Get<List<Word>>("api/Words");
            var words = wordsInDB.Where(x => x.CategoryId == Category.Id).ToList();
            if (words.Count != 0)
                await App.Current.MainPage.Navigation.PushAsync(new BluredWordsCardsPage(Category, words));
        }
        catch
        {
            return;

        }
    }
}
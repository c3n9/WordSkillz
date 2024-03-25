using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Pages.MiniGamePages;
using WordSkillz.Tools;

namespace WordSkillz.Popup;

public partial class MiniGamesPopup : ContentView
{
    public Category Category { get; set; }
    SQLiteDbContext db;
    public MiniGamesPopup()
    {
        InitializeComponent();
        db = new SQLiteDbContext();
    }

    private async void BPlayViewWords_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
        var wordsInDB = await db.GetAllWord();
        var words = wordsInDB.Where(x => x.CategoryId == Category.Id).ToList();
        if (words.Count != 0)
            await App.Current.MainPage.Navigation.PushAsync(new WordCardsPage(Category, words));
    }

    private async void BPlayMatchWords_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
        var wordsInDB = await db.GetAllWord();
        var words = wordsInDB.Where(x => x.CategoryId == Category.Id).ToList();
        if (words.Count != 0)
            await App.Current.MainPage.Navigation.PushAsync(new MatchWordsCard(Category,words));
    }

    private async void BPlayHiddenWords_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
        var wordsInDB = await db.GetAllWord();
        var words = wordsInDB.Where(x => x.CategoryId == Category.Id).ToList();
        if (words.Count != 0)
            await App.Current.MainPage.Navigation.PushAsync(new BluredWordsCardsPage(Category,words));
    }
}
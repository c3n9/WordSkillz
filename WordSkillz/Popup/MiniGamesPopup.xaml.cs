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
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
        var words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == Category.Id));
        if (words.Count != 0)
            await App.Current.MainPage.Navigation.PushAsync(new BluredWordsCardsPage(Category));
    }

    private void BPlayMatchWords_Clicked(object sender, EventArgs e)
    {

    }
}
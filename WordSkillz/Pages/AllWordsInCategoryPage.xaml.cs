using Android.App.AppSearch;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WordSkillz.Models;
using WordSkillz.Tools;
using static Android.Provider.UserDictionary;

namespace WordSkillz.Pages;

public partial class AllWordsInCategoryPage : ContentPage, INotifyPropertyChanged
{
    Category contextCategory;
    public ObservableCollection<Word> Words { get; set; }
    public AllWordsInCategoryPage(Category category)
    {
        InitializeComponent();
        contextCategory = category;
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id));
        BindingContext = this;
        GlobalSettings.allWordsInCategoryPage = this;
    }

    private void Refresh()
    {
    }

    private async void BAddWords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddWordsPage(contextCategory));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var word = (sender as Button).BindingContext as Word;
        DataManager.RemoveWord(word);
        Words.Remove(word);
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var words = DataManager.AllWords.Where(x => !x.TranslatedWord.ToLower().Contains(SBWords.Text.ToLower()) && x.CategoryId == contextCategory.Id);
        foreach (var word in words)
        {
            Words.Remove(word);
        }
    }
}
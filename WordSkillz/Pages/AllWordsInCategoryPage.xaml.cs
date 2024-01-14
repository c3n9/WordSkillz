using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WordSkillz.Models;
using WordSkillz.Tools;
using WordSkillz.ViewModels;

namespace WordSkillz.Pages;

public partial class AllWordsInCategoryPage : ContentPage, INotifyPropertyChanged
{
    private AllWordsInCategoryPageViewModel dc => (BindingContext as AllWordsInCategoryPageViewModel);

    public AllWordsInCategoryPage()
    {
        InitializeComponent();
        GlobalSettings.allWordsInCategoryPage = this;
    }

    private void Refresh()
    {
    }

    private async void BAddWords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddWordsPage(dc.ContextCategory));
    }
}
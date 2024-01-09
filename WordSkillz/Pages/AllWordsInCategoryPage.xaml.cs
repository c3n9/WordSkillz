using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AllWordsInCategoryPage : ContentPage
{
    Category contextCategory;
    public ObservableCollection<Word> Words { get; set; }
	public AllWordsInCategoryPage(Category category)
	{
		InitializeComponent();
        contextCategory = category;
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id));
        BindingContext = this;
    }

    private void Refresh()
    {
    }

    private async void BAddWords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddWordsPage(contextCategory));
    }
}
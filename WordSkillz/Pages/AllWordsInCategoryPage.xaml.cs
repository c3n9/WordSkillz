using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AllWordsInCategoryPage : ContentPage
{
    Category contextCategory;
	public AllWordsInCategoryPage(Category category)
	{
		InitializeComponent();
        contextCategory = category; 
        Refresh();
	}

    private void Refresh()
    {
        CVWords.ItemsSource = DataManager.GetWords().ToList();
    }

    private async void BAddWords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddWordsPage(contextCategory));
    }
}
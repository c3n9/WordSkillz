using Microsoft.Maui.ApplicationModel;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            LVCategories.ItemsSource = DataManager.GetCategories();
        }

        private async void BPlusCategory_Clicked(object sender, EventArgs e)
        {
            var nameCategory = await DisplayPromptAsync("New category", "Enter name of category", "Ok", "Cancel", "Name", 50);
            if (!string.IsNullOrWhiteSpace(nameCategory))
            {
                var category = new Category() { Id = DataManager.GetCategories().LastOrDefault().Id + 1, Name = nameCategory };
                DataManager.SetCategory(category);
                await Navigation.PushModalAsync(new AllWordsInCategoryPage(category));
            }
        }

        private async void LVCategories_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (LVCategories.SelectedItem is Category category)
            {
                LVCategories.SelectedItem = null;
                await Navigation.PushAsync(new AllWordsInCategoryPage(category));
            }
        }
    }
}
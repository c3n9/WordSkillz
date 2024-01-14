using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Tools;
using WordSkillz.ViewModels;

namespace WordSkillz.Pages
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Category> Categories { get; set; }
        public MainPage()
        {
            InitializeComponent();
            Categories = DataManager.AllCategories;
            BindingContext = this;
        }

        private void Refresh()
        {
            
        }

        private async void BPlusCategory_Clicked(object sender, EventArgs e)
        {
            var nameCategory = await DisplayPromptAsync("New category", "Enter name of category", "Ok", "Cancel", "Name", 50);
            if (!string.IsNullOrWhiteSpace(nameCategory))
            {
                var category = new Category() { Id = DataManager.GetCategories().LastOrDefault().Id + 1, Name = nameCategory };
                DataManager.SetCategory(category);
                Categories.Add(category);
                await Navigation.PushModalAsync(new AllWordsInCategoryPage() { BindingContext = new AllWordsInCategoryPageViewModel(category) });
            }
        }

        private async void LVCategories_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (LVCategories.SelectedItem is Category category)
            {
                LVCategories.SelectedItem = null;
                await Navigation.PushAsync(new AllWordsInCategoryPage() { BindingContext = new AllWordsInCategoryPageViewModel(category) });
            }
        }

        private void BRemove_Clicked(object sender, EventArgs e)
        {
            var category = (sender as Button).BindingContext as Category;
            DataManager.RemoveCategory(category);
            Categories.Remove(category);
        }
    }
}
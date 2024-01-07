using Microsoft.Maui.ApplicationModel;
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
            CVCategories.ItemsSource = DataManager.GetCategories();
        }

        private async void BPlusCategory_Clicked(object sender, EventArgs e)
        {
            var nameCategory = await DisplayPromptAsync("New category", "Enter name of category", "Ok", "Cancel", "Name", 50);
            if (!string.IsNullOrWhiteSpace(nameCategory))
            {
                await Navigation.PushModalAsync(new AddWordsPage(nameCategory));
            }
        }
    }
}
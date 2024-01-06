using Microsoft.Maui.ApplicationModel;
using WordSkillz.Tools;

namespace WordSkillz.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            CVCategories.ItemsSource = DataManager.GetCategories();
        }

        private async void BPlusCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddWordsPage());
        }
    }
}
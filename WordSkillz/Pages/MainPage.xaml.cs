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
            await Navigation.PushModalAsync(new AddWordsPage());
        }
    }
}
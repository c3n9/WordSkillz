using Microsoft.Maui.ApplicationModel;

namespace WordSkillz.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void BPlusCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddWordsPage());
        }
    }
}
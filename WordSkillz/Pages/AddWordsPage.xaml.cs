namespace WordSkillz.Pages;

public partial class AddWordsPage : ContentPage
{
	public AddWordsPage()
	{
		InitializeComponent();
	}

    private async void BBack_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}
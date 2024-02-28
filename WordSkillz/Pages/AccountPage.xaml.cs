namespace WordSkillz.Pages;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
	}
    protected async override void OnAppearing()
    {

    }

    private async void BLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }
}
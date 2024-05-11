namespace WordSkillz.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new AppShell();
    }

    private void TapGestureRecognizerRegister_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new RegistrationPage();
    }
}
using WordSkillz.Models;

namespace WordSkillz.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var user = (await NetManager.Get<List<User>>("api/Users")).FirstOrDefault(x => x.Email == EmailEntry.Text && x.Password == PasswordEntry.Text);
        if (user == null)
            return;
        Preferences.Set("userId", user.Id);
        App.loggedUser = user;
		App.Current.MainPage = new AppShell();
    }

    private void TapGestureRecognizerRegister_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new RegistrationPage();
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        Authorize();

    }

    private async void Authorize()
    {
        var userId = Preferences.Get("userId", 0);
        var user = (await NetManager.Get<List<User>>("api/Users")).FirstOrDefault(x => x.Id == userId);
        if (user != null)
        {
            App.loggedUser = user;
            App.Current.MainPage = new AppShell();
        }
    }
}
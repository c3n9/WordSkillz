using WordSkillz.Models;

namespace WordSkillz.Pages;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
        Authorization();
    }
    private async void Authorization()
    {
        await Task.Delay(3000);
        try
        {
            var userId = Preferences.Get("userId", 0);
            if (userId != 0)
            {

                var user = (await NetManager.Get<List<User>>("api/Users")).FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    App.loggedUser = user;
                    App.Current.MainPage = new AppShell();
                }
            }
            else
                App.Current.MainPage = new LoginPage();
    }
        catch(Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
}

    }
}
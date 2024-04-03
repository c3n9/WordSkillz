namespace WordSkillz.Pages;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();

    }
    //protected async override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    try
    //    {
    //        if (App.Account != null)
    //        {
    //            BindingContext = App.Account;
    //        }
    //    }
    //    catch
    //    {
    //        return;
    //    }
        
    //}

    private async void BLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegistrationPage());
    }
}
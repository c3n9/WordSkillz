namespace WordSkillz.Pages;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();

    }
    public async void UpdateAccount()
    {
        var db = new SQLiteDbContext();
        App.Account = await db.GetAccountAsync(1);
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (App.Account != null)
        {
            BindingContext = App.Account;
        }
    }

    private async void BLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegistrationPage());
    }
}
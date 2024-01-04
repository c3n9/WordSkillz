using Microsoft.Maui.ApplicationModel;

namespace WordSkillz.Pages;

public partial class OptionPage : ContentPage
{
	public OptionPage()
    {
		InitializeComponent();
        SThemeDark.IsToggled = Application.Current.RequestedTheme == AppTheme.Dark;
    }

    private void SThemeDark_Toggled(object sender, ToggledEventArgs e)
    {
        if (SThemeDark.IsToggled == true)
            Application.Current.UserAppTheme = AppTheme.Dark;
        else
            Application.Current.UserAppTheme = AppTheme.Light;
    }
}
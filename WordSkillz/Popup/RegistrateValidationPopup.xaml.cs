namespace WordSkillz.Popup;

public partial class RegistrateValidationPopup : ContentView
{
	public RegistrateValidationPopup(string message)
	{
		InitializeComponent();
        LMessage.Text = message;
	}

    private void BOk_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
    }
}
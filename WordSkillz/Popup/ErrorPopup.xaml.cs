namespace WordSkillz.Popup;

public partial class ErrorPopup : ContentView
{
	public ErrorPopup()
	{
		InitializeComponent();
	}

    private void BOk_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
    }
}
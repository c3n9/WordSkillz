namespace WordSkillz.Popup;

public partial class NoConnectionPopup : ContentView
{
	public NoConnectionPopup()
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
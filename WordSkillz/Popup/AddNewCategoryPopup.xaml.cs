using WordSkillz.Models;
using WordSkillz.Pages;
using WordSkillz.Tools;

namespace WordSkillz.Popup;

public partial class AddNewCategoryPopup : ContentView
{
    public AddNewCategoryPopup()
    {
        InitializeComponent();
    }

    private void BCancel_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
    }

    private async void BOk_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(nameCategory.Text))
            {
                var category = new Category() { Name = nameCategory.Text };
                await NetManager.Post("api/Categories", category);
                if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
                {
                    parentPopup.Close();
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
}
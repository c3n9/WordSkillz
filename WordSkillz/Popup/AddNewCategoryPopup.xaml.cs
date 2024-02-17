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
        if (!string.IsNullOrWhiteSpace(nameCategory.Text))
        {
            var category = new Category() { Id = DataManager.GetCategories().LastOrDefault().Id + 1, Name = nameCategory.Text };
            DataManager.SetCategory(category);
            if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
            {
                parentPopup.Close();
            }
        }
    }
}
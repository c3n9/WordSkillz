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
            var category = new Category() { Id = DataManager.AllCategories.LastOrDefault().Id + 1, Name = nameCategory.Text };
            DataManager.AllCategories.Add(category);
            DataManager.AllCategories = DataManager.AllCategories;
            if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
            {
                parentPopup.Close();
            }
        }
    }
}
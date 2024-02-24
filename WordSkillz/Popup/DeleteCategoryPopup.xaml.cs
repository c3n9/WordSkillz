using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Popup;

public partial class DeleteCategoryPopup : ContentView
{
    public Category Category { get; set; }
	public DeleteCategoryPopup()
	{
		InitializeComponent();
	}

    private void BNo_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
    }

    private void BYes_Clicked(object sender, EventArgs e)
    {
        var words = DataManager.AllWords.Where(x => x.CategoryId == Category.Id).ToList();
        foreach(var word in words)
        {
            DataManager.AllWords.Remove(word);
            DataManager.AllWords = DataManager.AllWords;
        }
        DataManager.AllCategories.Remove(Category);
        DataManager.AllCategories = DataManager.AllCategories;
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }

    }
}
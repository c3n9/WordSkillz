using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Popup;

public partial class DeleteCategoryPopup : ContentView
{
    SQLiteDbContext db;
    public Category Category { get; set; }
	public DeleteCategoryPopup()
	{
		InitializeComponent();
        db = new SQLiteDbContext();
	}

    private void BNo_Clicked(object sender, EventArgs e)
    {
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }
    }

    private async void BYes_Clicked(object sender, EventArgs e)
    {
        var wordsInDatabase = await db.GetAllWord();
        var words = wordsInDatabase.Where(x => x.CategoryId == Category.Id).ToList();
        foreach(var word in words)
        {
            await db.DeleteWordAsync(word);
        }
        await db.DeleteCategoryAsync(Category);
        if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
        {
            parentPopup.Close();
        }

    }
}
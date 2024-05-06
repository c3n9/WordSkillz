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

    private async void BYes_Clicked(object sender, EventArgs e)
    {
        try
        {
            var wordsInDatabase = await NetManager.Get<List<Word>>("api/Words");
            var words = wordsInDatabase.Where(x => x.CategoryId == Category.Id).ToList();
            foreach (var word in words)
            {
                await NetManager.Delete<bool>($"api/Words/{word.Id}");
            }
            await NetManager.Delete<bool>($"api/Categories/{Category.Id}");

            if (Parent is CommunityToolkit.Maui.Views.Popup parentPopup)
            {
                parentPopup.Close();
            }
        }
        catch
        {

        }

    }
}
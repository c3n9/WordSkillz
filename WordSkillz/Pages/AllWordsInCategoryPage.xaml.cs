using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AllWordsInCategoryPage : ContentPage
{
    Category contextCategory;
    SQLiteDbContext db;
    public List<Word> Words { get; set; }

    public AllWordsInCategoryPage(Category category)
    {
        InitializeComponent();
        contextCategory = category;
        db = new SQLiteDbContext();
        // Начать асинхронную операцию задержки
        DelayedRefresh();
    }

    // Асинхронный метод для задержки
    private async Task DelayedRefresh()
    {
        // Подождать 2 секунды перед вызовом Refresh()
        ActivityIndicator.IsRunning = true;
        ActivityIndicator.IsVisible = true;
        await Task.Delay(500);
        Refresh();
    }

    private async void Refresh()
    {
        LVWords.ItemsSource = null;
        var wordsInDB = await db.GetAllWord();
        Words = wordsInDB.Where(x => x.CategoryId == contextCategory.Id).ToList();
        LVWords.ItemsSource = Words;

        ActivityIndicator.IsRunning = false;
        ActivityIndicator.IsVisible = false;
    }

    private async void BAddWords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWordsPage(contextCategory));
    }

    private async void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        if (e.IsOpen)
        {
            var item = (Word)((SwipeView)sender).BindingContext;
            await db.DeleteWordAsync(item);
            Refresh();
        }
    }

    private void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        if (e.Offset > 0)
        {
            e.Offset = 0;
        }
    }

    private void LVWords_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        LVWords.SelectedItem = null;
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        // При появлении страницы необходимо вызвать DelayedRefresh, чтобы начать задержку перед обновлением
        DelayedRefresh();
    }
}
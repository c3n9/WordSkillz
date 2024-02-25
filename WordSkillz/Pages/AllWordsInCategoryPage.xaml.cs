using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AllWordsInCategoryPage : ContentPage
{
    Category contextCategory;
    public List<Word> Words { get; set; }
    public AllWordsInCategoryPage(Category category)
    {
        InitializeComponent();
        contextCategory = category;
        Refresh();
    }

    private async void Refresh()
    {
        LVWords.ItemsSource = null;
        Words = DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id).ToList();
        LVWords.ItemsSource = Words;
        BindingContext = this;
        GlobalSettings.allWordsInCategoryPage = this;
    }

    private async void BAddWords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWordsPage(contextCategory));
    }
    private void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        if (e.IsOpen)
        {
            // Пользователь смахнул до конца, удаляем элемент
            var item = (Word)((SwipeView)sender).BindingContext;
            DataManager.AllWords.Remove(item);
            DataManager.AllWords = DataManager.AllWords;
            Refresh();
        }
    }
    private void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        if (e.Offset > 0) // Пользователь смахивает вправо
        {
            e.Offset = 0; // Запретить открытие действий при смахивании вправо
        }
    }

    private void LVWords_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        LVWords.SelectedItem = null;
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        Refresh();
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AllWordsInCategoryPage : ContentPage, INotifyPropertyChanged
{
    Category contextCategory;
    public ObservableCollection<Word> Words { get; set; }
    public AllWordsInCategoryPage(Category category)
    {
        InitializeComponent();
        contextCategory = category;
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == contextCategory.Id));
        BindingContext = this;
        GlobalSettings.allWordsInCategoryPage = this;
    }

    private void Refresh()
    {
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
            (LVWords.ItemsSource as ObservableCollection<Word>).Remove(item);
            DataManager.RemoveWord(item);
        }
    }
    private void OnSwipeChanging(object sender, SwipeChangingEventArgs e)
    {
        if (e.Offset > 0) // Пользователь смахивает вправо
        {
            e.Offset = 0; // Запретить открытие действий при смахивании вправо
        }
    }
}
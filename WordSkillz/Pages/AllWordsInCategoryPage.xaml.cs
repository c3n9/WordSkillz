using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WordSkillz.Models;
using WordSkillz.Popup;
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
        // Начать асинхронную операцию задержки
        DelayedRefresh();
    }

    // Асинхронный метод для задержки
    private async void DelayedRefresh()
    {
        // Подождать 2 секунды перед вызовом Refresh()
        ActivityIndicator.IsRunning = true;
        ActivityIndicator.IsVisible = true;
        await Task.Delay(500);
        Refresh();
    }

    private async void Refresh()
    {
        try
        {
            Words = (await NetManager.Get<List<Word>>("api/Words")).Where(x => x.CategoryId == contextCategory.Id).ToList();
            BindingContext = this;
            LVWords.ItemsSource = Words;

            ActivityIndicator.IsRunning = false;
            ActivityIndicator.IsVisible = false;
        }
        catch (Exception ex)
        {
            
        }

    }

    private async void BAddWords_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWordsPage(contextCategory));
    }

    private async void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        try
        {
            if (e.IsOpen)
            {
                var item = (Word)((SwipeView)sender).BindingContext;
                await NetManager.Delete<bool>($"api/Words/{item.Id}");
                Refresh();
            }

        }
        catch (Exception ex)
        {
            var noConnectionPopup = new NoConnectionPopup();
            var popup = new CommunityToolkit.Maui.Views.Popup();
            popup.Content = noConnectionPopup;
            popup.Color = Color.FromRgba(0, 0, 0, 0);
            popup.Closed += (s, args) =>
            {

            };
            App.Current.MainPage.ShowPopup(popup);
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
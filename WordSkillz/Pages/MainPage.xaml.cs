using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Pages.MiniGamePages;
using WordSkillz.Popup;
using WordSkillz.Tools;

namespace WordSkillz.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DelayedRefresh();
        }
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
            try
            {
                BindingContext = this;
                var categories = await NetManager.Get<List<Category>>("api/Categories");
                foreach (var cateogory in categories)
                {
                    cateogory.WordCount = (await NetManager.Get<List<Word>>("api/Words")).Where(x => x.CategoryId == cateogory.Id).Count();
                }
                LVCategories.ItemsSource = categories;
                ActivityIndicator.IsRunning = false;
                ActivityIndicator.IsVisible = false;
            }
            catch (Exception ex)
            {
                var noConnectionPopup = new NoConnectionPopup();
                var popup = new CommunityToolkit.Maui.Views.Popup();
                popup.Content = noConnectionPopup;
                popup.Color = Color.FromRgba(0, 0, 0, 0);
                App.Current.MainPage.ShowPopup(popup);
            }
        }
        private void BPlusCategory_Clicked(object sender, EventArgs e)
        {
            var addNewCategoryPopup = new AddNewCategoryPopup();
            // Создать экземпляр CommunityToolkit.Maui.Views.Popup и установить его содержимое
            var popup = new CommunityToolkit.Maui.Views.Popup();
            popup.Content = addNewCategoryPopup;
            //popup.Color = Color.FromArgb("#ebecf0");
            popup.Color = Color.FromRgba(0, 0, 0, 0);
            // Отобразить попап
            popup.Closed += (s, args) =>
            {
                // Выполняем обновление данных после закрытия Popup
                Refresh();
            };
            App.Current.MainPage.ShowPopup(popup);
        }

        [Obsolete]
        private async void LVCategories_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (LVCategories.SelectedItem is Category category)
            {
                LVCategories.SelectedItem = null;
                await Navigation.PushAsync(new AllWordsInCategoryPage(category));
            }
        }
        private async void BLearn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    Category selectedCategory = button.BindingContext as Category;

                    var miniGamesPopup = new MiniGamesPopup();
                    var popupGames = new CommunityToolkit.Maui.Views.Popup();
                    popupGames.Content = miniGamesPopup;
                    popupGames.Color = Color.FromRgba(0, 0, 0, 0);
                    miniGamesPopup.Category = selectedCategory;
                    App.Current.MainPage.ShowPopup(popupGames);
                    popupGames.Closed += async (s, args) =>
                    {
                        var wordsInDB = await NetManager.Get<List<Word>>("api/Words");
                        var words = new ObservableCollection<Word>(wordsInDB.Where(x => x.CategoryId == selectedCategory.Id));
                        if (words.Count == 0)
                        {
                            var errorPopup = new ErrorPopup();
                            var popup = new CommunityToolkit.Maui.Views.Popup();
                            popup.Content = errorPopup;
                            popup.Color = Color.FromRgba(0, 0, 0, 0);
                            App.Current.MainPage.ShowPopup(popup);
                            return;
                        }
                        LVCategories.SelectedItem = null;
                    };
                }
            }
            catch (Exception ex)
            {
                var noConnectionPopup = new NoConnectionPopup();
                var popup = new CommunityToolkit.Maui.Views.Popup();
                popup.Content = noConnectionPopup;
                popup.Color = Color.FromRgba(0, 0, 0, 0);
                App.Current.MainPage.ShowPopup(popup);
            }
        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            if (e.IsOpen)
            {
                var deleteCategoryPopup = new DeleteCategoryPopup();
                // Создать экземпляр CommunityToolkit.Maui.Views.Popup и установить его содержимое
                var popup = new CommunityToolkit.Maui.Views.Popup();
                popup.Content = deleteCategoryPopup;
                popup.Color = Color.FromRgba(0, 0, 0, 0);
                var item = (Category)((SwipeView)sender).BindingContext;
                deleteCategoryPopup.Category = item;
                // Отобразить попап
                popup.Closed += async (s, args) =>
                {
                    // Выполняем обновление данных после закрытия Popup
                    Refresh();
                };
                App.Current.MainPage.ShowPopup(popup);
                ((SwipeView)sender).Close();
            }
        }

        private void SwipeView_SwipeChanging(object sender, SwipeChangingEventArgs e)
        {
            if (e.Offset > 0) // Пользователь смахивает вправо
            {
                e.Offset = 0; // Запретить открытие действий при смахивании вправо
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var tutorialPopup = new TutorialPopup();
            // Создать экземпляр CommunityToolkit.Maui.Views.Popup и установить его содержимое
            var popup = new CommunityToolkit.Maui.Views.Popup();
            popup.Content = tutorialPopup;
            popup.Color = Color.FromRgba(0, 0, 0, 0);

            // Отобразить попап
            App.Current.MainPage.ShowPopup(popup);
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            DelayedRefresh();
        }
    }
}
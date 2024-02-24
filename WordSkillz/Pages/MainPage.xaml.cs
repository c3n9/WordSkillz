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
        public List<Category> Categories { get; set; }
        public MainPage()
        {
            InitializeComponent();
            Categories = DataManager.AllCategories;
            BindingContext = this;
            Refresh();
        }

        private void Refresh()
        {
            LVCategories.ItemsSource = null;
            LVCategories.ItemsSource = DataManager.AllCategories;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
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
            popup.Closed += async (s, args) =>
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
                    var words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == selectedCategory.Id));
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

        private async void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
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
    }
}
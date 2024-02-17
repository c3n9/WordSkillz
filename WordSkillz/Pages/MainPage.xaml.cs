﻿using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Pages.MiniGamePages;
using WordSkillz.Popup;
using WordSkillz.Tools;

namespace WordSkillz.Pages
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Category> Categories { get; set; }
        public MainPage()
        {
            InitializeComponent();
            Categories = DataManager.AllCategories;
            BindingContext = this;

            Refresh();
        }

        private async void Refresh()
        {
            LVCategories.ItemsSource = DataManager.AllCategories;
        }

        private async void BPlusCategory_Clicked(object sender, EventArgs e)
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
            App.Current.MainPage.ShowPopup(popup);
        }

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
            var miniGame = await DisplayActionSheet("MiniGames", null, null, "Viewing words");
            if (miniGame == "Viewing words")
            {
                if (sender is Button button)
                {
                    // Получаем BindingContext из родительского элемента кнопки
                    Category selectedCategory = button.BindingContext as Category;

                    if (selectedCategory != null)
                    {
                        var words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == selectedCategory.Id));
                        if (words.Count == 0)
                        {
                            await DisplayAlert("Error", "There are no words in this category", "Ok");
                            return;
                        }
                        // Выполняйте необходимые действия с выбранной категорией
                        LVCategories.SelectedItem = null;
                        await Navigation.PushAsync(new WordCardsPage(selectedCategory));
                    }
                }
            }
        }

        private async void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            if (e.IsOpen)
            {
                var answer = await DisplayAlert("Warning", "Delete this category?", "Yes", "No");
                if (answer)
                {
                    // Пользователь смахнул до конца, удаляем элемент
                    var item = (Category)((SwipeView)sender).BindingContext;
                    (LVCategories.ItemsSource as ObservableCollection<Category>).Remove(item);
                    DataManager.RemoveWords(DataManager.AllWords.Where(x => x.CategoryId == item.Id).ToList());
                    DataManager.RemoveCategory(item);
                }
                else
                {
                    ((SwipeView)sender).Close();
                }
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
            Refresh();
        }
    }
}
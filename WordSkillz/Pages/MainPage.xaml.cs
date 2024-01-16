﻿using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;
using WordSkillz.Models;
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
        }

        private void Refresh()
        {

        }

        private async void BPlusCategory_Clicked(object sender, EventArgs e)
        {
            var nameCategory = await DisplayPromptAsync("New category", "Enter name of category", "Ok", "Cancel", "Name", 50);
            if (!string.IsNullOrWhiteSpace(nameCategory))
            {
                var category = new Category() { Id = DataManager.GetCategories().LastOrDefault().Id + 1, Name = nameCategory };
                DataManager.SetCategory(category);
                Categories.Add(category);
                await Navigation.PushAsync(new AllWordsInCategoryPage(category));
            }
        }

        private async void LVCategories_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (LVCategories.SelectedItem is Category category)
            {
                LVCategories.SelectedItem = null;
                await Navigation.PushAsync(new AllWordsInCategoryPage(category));
            }
        }

        private void BRemove_Clicked(object sender, EventArgs e)
        {
            var category = (sender as Button).BindingContext as Category;
            DataManager.RemoveCategory(category);
            Categories.Remove(category);
        }

        private void BLearn_Clicked(object sender, EventArgs e)
        {

        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            if (e.IsOpen)
            {
                // Пользователь смахнул до конца, удаляем элемент
                var item = (Category)((SwipeView)sender).BindingContext;
                (LVCategories.ItemsSource as ObservableCollection<Category>).Remove(item);
                DataManager.RemoveCategory(item);
            }
        }

        private void SwipeView_SwipeChanging(object sender, SwipeChangingEventArgs e)
        {
            if (e.Offset > 0) // Пользователь смахивает вправо
            {
                e.Offset = 0; // Запретить открытие действий при смахивании вправо
            }
        }
    }
}
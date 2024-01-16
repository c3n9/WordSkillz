using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class WordCardsPage : ContentPage
{
    private int currentIndex = 0;
    public ObservableCollection<Word> Words { get; set; }

    public WordCardsPage(Category category)
    {
        InitializeComponent();
        Words = new ObservableCollection<Word>(DataManager.AllWords.Where(x => x.CategoryId == category.Id));
        BindingContext = this;

        // Устанавливаем начальный источник данных, включая только один элемент
        LVWordСards.ItemsSource = Words.Take(1);
    }
    private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        currentIndex++;
        if (currentIndex >= Words.Count)
        {
            currentIndex = 0;
        }
        LVWordСards.ItemsSource = Words.Skip(currentIndex).Take(1);
    }

    private void SwipeView_SwipeChanging(object sender, SwipeChangingEventArgs e)
    {

    }
}

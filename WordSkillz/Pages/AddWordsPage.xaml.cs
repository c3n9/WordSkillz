using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AddWordsPage : ContentPage
{
    Category contextCategory;
    List<Tuple<string, string>> words;

    public AddWordsPage(Category category)
    {
        InitializeComponent();
        NewWord();
        contextCategory = category;
        words = new List<Tuple<string, string>>();
    }

    private async void NewWord()
    {
        var frame = new Frame
        {
            CornerRadius = 20,
            Opacity = 0,
            Scale = 0.8,
            Margin = new Thickness(20),
            Content = new VerticalStackLayout
            {
                Children =
            {
                new Entry { WidthRequest = 200, Placeholder="Original", ReturnType = ReturnType.Next, ReturnCommand = new Command(() => FocusTranslateEntry()) },
                new Entry { WidthRequest = 200, Placeholder="Translate", ReturnType = ReturnType.Done, ReturnCommand = new Command(() => NewWord()) }
            }
            }
        };

        // Применение стиля для Frame
        frame.Style = (Style)this.Resources["MyFrameStyle"];

        // Применение стиля для Entry
        foreach (var child in ((VerticalStackLayout)frame.Content).Children)
        {
            if (child is Entry entry)
            {
                entry.Style = (Style)this.Resources["MyEntryStyle"];
            }
        }

        VSLWords.Children.Add(frame);

        // Анимация появления
        await frame.FadeTo(1, 250, Easing.Linear);
        await frame.ScaleTo(1, 250, Easing.SpringOut);

        var originalEntry = ((VerticalStackLayout)frame.Content).Children.OfType<Entry>().FirstOrDefault();
        originalEntry?.Focus();
    }




    private void FocusTranslateEntry()
    {
        var currentFrameIndex = VSLWords.Children.IndexOf(VSLWords.Children.LastOrDefault());
        var nextFrame = VSLWords.Children.ElementAtOrDefault(currentFrameIndex + 1) as Frame;

        if (nextFrame != null)
        {
            var translateEntry = ((VerticalStackLayout)nextFrame.Content).Children.OfType<Entry>().FirstOrDefault();
            translateEntry?.Focus();
        }
    }
    private void FocusOriginalEntryOfNextWordPair()
    {
        var currentFrameIndex = VSLWords.Children.IndexOf(VSLWords.Children.LastOrDefault());
        var nextFrame = VSLWords.Children.ElementAtOrDefault(currentFrameIndex + 1) as Frame;

        if (nextFrame != null)
        {
            var originalEntry = ((VerticalStackLayout)nextFrame.Content).Children.OfType<Entry>().FirstOrDefault();
            originalEntry?.Focus();
        }
    }
    private void BAddWord_Clicked(object sender, EventArgs e)
    {
        var buttonToRemove = VSLWords.Children.OfType<Button>().FirstOrDefault(b => b.Text == "+");
        if (buttonToRemove != null)
        {
            VSLWords.Children.Remove(buttonToRemove);
        }
        NewWord();
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        words.Clear();
        Entry previousEntry = null;

        foreach (var childInVSL in VSLWords.Children)
        {
            if (childInVSL is Frame frame)
            {
                foreach (var childInFrame in frame.Children)
                {
                    if (childInFrame is VerticalStackLayout VSLInFrame)
                    {
                        foreach (var childInVSLInFrame in VSLInFrame.Children)
                        {
                            if (childInVSLInFrame is Entry entry)
                            {
                                if (previousEntry != null)
                                {
                                    words.Add(new Tuple<string, string>(previousEntry.Text, entry.Text));
                                    previousEntry = null;
                                }
                                else
                                {
                                    previousEntry = entry;
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (var wordPair in words)
        {
            if (!string.IsNullOrWhiteSpace(wordPair.Item1) || !string.IsNullOrWhiteSpace(wordPair.Item2))
            {
                var newWord = new Word()
                {
                    Id = DataManager.AllWords.Any() ? DataManager.AllWords.Max(w => w.Id) + 1 : 1,
                    OriginalWord = wordPair.Item1,
                    TranslatedWord = wordPair.Item2,
                    CategoryId = contextCategory.Id
                };
                DataManager.AllWords.Add(newWord);
                DataManager.AllWords = DataManager.AllWords;
                GlobalSettings.allWordsInCategoryPage.Words.Add(newWord);
            }
        }

        VSLWords.Children.Clear();
        NewWord();
    }
}
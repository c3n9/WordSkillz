using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AddWordsPage : ContentPage
{
    Category contextCategory;
    List<Tuple<string, string>> words; // Use a list to store original and translated word pairs

    public AddWordsPage(Category category)
    {
        InitializeComponent();
        NewWord();
        contextCategory = category;
        words = new List<Tuple<string, string>>();
    }

    private void NewWord()
    {
        var frame = new Frame
        {
            CornerRadius = 20,
            BorderColor = Color.FromArgb("#512BD4"),
            Margin = new Thickness(30),
            Content = new VerticalStackLayout
            {
                Children =
            {
                new Entry { WidthRequest = 200, Placeholder="Original", TextColor=Color.FromArgb("#512BD4"), ReturnType = ReturnType.Next, ReturnCommand = new Command(() => FocusTranslateEntry()) },
                new Entry { WidthRequest = 200, Placeholder="Translate", TextColor=Color.FromArgb("#512BD4"), ReturnType = ReturnType.Done, ReturnCommand = new Command(() => NewWord()) }
            }
            }
        };
        VSLWords.Children.Add(frame);
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
                DataManager.SetWord(newWord);
                GlobalSettings.allWordsInCategoryPage.Words.Add(newWord);
            }
        }

        VSLWords.Children.Clear();
        NewWord();
    }
}
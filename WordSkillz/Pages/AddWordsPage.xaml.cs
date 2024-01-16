using System.Collections.ObjectModel;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class AddWordsPage : ContentPage
{
    Category contextCategory;
    Dictionary<string, string> words;
    public AddWordsPage(Category category)
    {
        InitializeComponent();
        NewWord();
        contextCategory = category;
        words = new Dictionary<string, string>();
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
                    new Entry { WidthRequest = 200, Placeholder="Original", TextColor=Color.FromArgb("#512BD4")},
                    new Entry { WidthRequest = 200, Placeholder="Translate", TextColor=Color.FromArgb("#512BD4")}
                }
            }
        };
        VSLWords.Children.Add(frame);
        var button = new Button
        {
            Text = "+",
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10),
            CornerRadius = 20
        };
        button.Clicked += BAddWord_Clicked;
        VSLWords.Children.Add(button);
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
                                    words[previousEntry.Text] = entry.Text;
                                    previousEntry = null;
                                }
                                else
                                {
                                    previousEntry = entry;
                                    words.Add(previousEntry.Text, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (var word in words)
        {
            var newWord = new Word()
            {
                Id = DataManager.AllWords.Any() ? DataManager.AllWords.Last().Id + 1 : 1,
                OriginalWord = word.Key,
                TranslatedWord = word.Value,
                CategoryId = contextCategory.Id
            };

            DataManager.SetWord(newWord);
            GlobalSettings.allWordsInCategoryPage.Words.Add(newWord);
        }
    }
}
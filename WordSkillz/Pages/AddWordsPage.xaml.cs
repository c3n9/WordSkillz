using WordSkillz.Models;

namespace WordSkillz.Pages;

public partial class AddWordsPage : ContentPage
{
    Category contextCategory; 
    public AddWordsPage(Category category)
    {
        InitializeComponent();
        NewWord();
        contextCategory = category;
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
                    new Entry { WidthRequest = 200, Placeholder="Translate", TextColor=Color.FromArgb("#512BD4")},
                    new Button
                    {
                        Text = "Save",
                        HorizontalOptions = LayoutOptions.End,
                        BackgroundColor = Color.FromArgb("#512BD4"),
                        TextColor= Color.FromArgb("#fff"),
                        WidthRequest = 200,
                        CornerRadius = 10
                    }
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

    private async void BBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
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
}
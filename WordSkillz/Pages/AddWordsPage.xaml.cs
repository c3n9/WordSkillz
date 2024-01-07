namespace WordSkillz.Pages;

public partial class AddWordsPage : ContentPage
{
    public AddWordsPage(string nameCategory)
    {
        InitializeComponent();
        Title = nameCategory;
        NewWord();
    }


    private void NewWord()
    {
        var frame = new Frame
        {
            CornerRadius = 20,
            BorderColor = Color.FromArgb("#512BD4"),
            Margin = new Thickness(10),
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Entry { WidthRequest = 200, Placeholder="Original", TextColor=Color.FromArgb("#512BD4")},
                    new Entry { WidthRequest = 200, Placeholder="Translate", TextColor=Color.FromArgb("#512BD4")},
                    new Button
                    {
                        Text = "Save",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        BackgroundColor = Color.FromArgb("#512BD4"),
                        TextColor= Color.FromArgb("#fff"),
                        WidthRequest = 200,
                        CornerRadius = 10
                    }
                }
            }
        };
        VSLWords.Children.Add(frame);
    }

    private async void BBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void BAddWord_Clicked(object sender, EventArgs e)
    {
        NewWord();
    }
}
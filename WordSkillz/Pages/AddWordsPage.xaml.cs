namespace WordSkillz.Pages;

public partial class AddWordsPage : ContentPage
{
	public AddWordsPage()
	{
		InitializeComponent();
        NewWord();
	}

    private void NewWord()
    {
        var frame = new Frame
        {
            CornerRadius = 20,
            BorderColor = Color.FromHex("#512BD4"),
            Margin = new Thickness(10),
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Entry { WidthRequest = 200 },
                    new Entry { WidthRequest = 200 },
                    new Button
                    {
                        Text = "Save",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
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
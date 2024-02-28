namespace WordSkillz.Pages;

public partial class RegistrationPage : ContentPage
{
    private string selectedPhotoPath;
    public RegistrationPage()
    {
        InitializeComponent();
    }
    private async void Register_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    { 
        await Navigation.PopModalAsync();
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        var photoResult = await MediaPicker.PickPhotoAsync();

        if (photoResult != null)
        {
            selectedPhotoPath = photoResult.FullPath;
            SelectedPhoto.Source = ImageSource.FromFile(selectedPhotoPath);
        }
    }
}
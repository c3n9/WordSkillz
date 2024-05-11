namespace WordSkillz.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizerLogin_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }

    private async void TapGestureRecognizerImage_Tapped(object sender, TappedEventArgs e)
    {
        var dialog = await MediaPicker.PickPhotoAsync();

        if (dialog != null)
        {
            var imageInBytes = File.ReadAllBytes(dialog.FullPath);
            var selectedPhotoPath = dialog.FullPath;
            SelectedPhoto.Source = ImageSource.FromFile(selectedPhotoPath);
        }
    }

    private void BRegister_Clicked(object sender, EventArgs e)
    {

    }
}
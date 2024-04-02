using WordSkillz.Models;

namespace WordSkillz.Pages;

public partial class RegistrationPage : ContentPage
{
    private string selectedPhotoPath;
    private Account contextAccount;
    SQLiteDbContext db;
    public RegistrationPage()
    {
        InitializeComponent();
        contextAccount = new Account();
        BindingContext = contextAccount;
        db = new SQLiteDbContext();
    }
    private async void Register_Clicked(object sender, EventArgs e)
    {
        await db.AddAccountAsync(contextAccount);
        App.Account = contextAccount;
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
using CommunityToolkit.Maui.Views;
using System.ComponentModel.DataAnnotations;
using WordSkillz.Models;
using WordSkillz.Popup;

namespace WordSkillz.Pages;

public partial class RegistrationPage : ContentPage
{
    User contextUser;
	public RegistrationPage()
	{
		InitializeComponent();
        contextUser = new User() { CorrectAnswersCount = 0, IncorrectAnswersCount = 0, LearnedWordsCount = 0};
        BindingContext = contextUser;
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
            contextUser.Image = File.ReadAllBytes(dialog.FullPath);
            var selectedPhotoPath = dialog.FullPath;
            SelectedPhoto.Source = ImageSource.FromFile(selectedPhotoPath);
        }
    }

    private async void BRegister_Clicked(object sender, EventArgs e)
    {
        try
        {
            var error = string.Empty;
            var validationContext = new ValidationContext(contextUser);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(contextUser, validationContext, results, true))
            {
                foreach (var result in results)
                {
                    error += $"{result.ErrorMessage}";
                }
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                return;
            }
            await NetManager.Post("api/Users", validationContext);
            App.Current.MainPage = new LoginPage();

        }
        catch
        {
            var noConnectionPopup = new NoConnectionPopup();
            var popup = new CommunityToolkit.Maui.Views.Popup();
            popup.Content = noConnectionPopup;
            popup.Color = Color.FromRgba(0, 0, 0, 0);
            App.Current.MainPage.ShowPopup(popup);
        }
    }
}
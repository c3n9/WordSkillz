using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;
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
        contextUser = new User() { CorrectAnswersCount = 0, IncorrectAnswersCount = 0, LearnedWordsCount = 0 };
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
                    error += $"{result.ErrorMessage}\n";
                }
            }
            if (!string.IsNullOrWhiteSpace(error))
            {
                var registrateValidationPopup = new RegistrateValidationPopup(error);
                var popup = new CommunityToolkit.Maui.Views.Popup();
                popup.Content = registrateValidationPopup;
                popup.Color = Color.FromRgba(0, 0, 0, 0);
                App.Current.MainPage.ShowPopup(popup);
                return;
            }
            var userDTO = ToUserDTO(contextUser);
            await NetManager.Post("api/Users", userDTO);
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
    public UserDTO ToUserDTO(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Image = user.Image,
            PhoneNumber = user.PhoneNumber,
            LearnedWordsCount = user.LearnedWordsCount,
            IncorrectAnswersCount = user.IncorrectAnswersCount,
            CorrectAnswersCount = user.CorrectAnswersCount
        };
    }
}
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz.Pages;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
        OnAppearing();
    }
    protected async void OnAppearing()
    {
        base.OnAppearing();
        List<Category> n;
        n = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json")));
        while (n == null)
        {
            if (File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json")))
            {
                File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json"));
            }
            DataManager.InitDataFile(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json"), DataManager.CategoryImportPath);
            await Task.Delay(200);
            n = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json")));
        }
        await Task.Delay(1050);
        //await Navigation.PushAsync(new MainPage());
        await Shell.Current.GoToAsync($"////{nameof(MainPage)}");
    }
}
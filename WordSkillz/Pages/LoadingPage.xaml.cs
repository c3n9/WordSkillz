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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        List<Category> categories;
        List<Word> words;
        categories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json")));
        words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, "wordCache.json")));
        if(categories != null && words != null)
        {
            await Shell.Current.GoToAsync($"////{nameof(MainPage)}");
        }
        else
        {
            while (categories == null)
            {
                if (File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json")))
                {
                    File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json"));
                }
                DataManager.InitDataFile(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json"), DataManager.CategoryImportPath);
                await Task.Delay(200);
                categories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json")));
            }
            while (words == null)
            {
                if (File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, "wordCache.json")))
                {
                    File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, "wordCache.json"));
                }
                DataManager.InitDataFile(Path.Combine(FileSystem.Current.AppDataDirectory, "wordCache.json"), DataManager.WordsImportPath);
                await Task.Delay(200);
                words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText(Path.Combine(FileSystem.Current.AppDataDirectory, "wordCache.json")));
            }
            await Task.Delay(1050);
            await Shell.Current.GoToAsync($"////{nameof(MainPage)}");
        }

    }
}
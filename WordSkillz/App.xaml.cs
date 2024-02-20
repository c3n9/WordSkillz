using WordSkillz.Tools;

namespace WordSkillz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json"));

            //File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, "wordCache.json"));
            Application.Current.UserAppTheme = AppTheme.Light;
            DataManager.InitDataFile(DataManager.CategoryCachePath, DataManager.CategoryImportPath);

            DataManager.InitDataFile(DataManager.WordCachePath, DataManager.WordsImportPath);

            MainPage = new AppShell();

        }
    }
}
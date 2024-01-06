using WordSkillz.Tools;

namespace WordSkillz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            try
            {
                var n = Path.Combine(FileSystem.Current.AppDataDirectory, "categoryCache.json");
            }
            catch (Exception ex)
            {
                var g = ex.Message;
            }
            DataManager.InitDataFile(DataManager.CategoryCachePath, DataManager.CategoryImportPath);
            DataManager.InitDataFile(DataManager.WordCachePath, DataManager.WordsImportPath);
        }
    }
}
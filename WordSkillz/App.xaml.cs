using WordSkillz.Tools;

namespace WordSkillz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            DataManager.InitDataFile(DataManager.CategoryCachePath, DataManager.CategoryImportPath);

            //DataManager.InitDataFile(DataManager.WordCachePath, DataManager.WordsImportPath);

            MainPage = new AppShell();

        }
    }
}
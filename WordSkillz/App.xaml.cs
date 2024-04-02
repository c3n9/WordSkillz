using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz
{
    public partial class App : Application
    {
        public static Account Account;
        public App()
        {
            InitializeComponent(); 
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();
        }
        
    }
}
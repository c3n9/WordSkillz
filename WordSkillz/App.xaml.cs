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
            //Authoriztation();
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();
        }
        //public async void Authoriztation()
        //{
        //    SQLiteDbContext db = new SQLiteDbContext();
        //    var loggedInAccountId = Preferences.Get("LoggedInAccountId", -1);
        //    if (loggedInAccountId != -1)
        //    {
        //        var account = await db.GetAllAccountsAsync();
        //        Account = await db.GetAccountAsync(loggedInAccountId);
        //    }
        //}
    }
}
using WordSkillz.Models;
using WordSkillz.Tools;

namespace WordSkillz
{
    public partial class App : Application
    {
        public static int LearnedWordsCount { get; set; }
        public static int CorrectAnswersCount { get; set; }
        public static int IncorrectAnswersCount { get; set; }
        public App()
        {
            InitializeComponent();
            LoadCounts();
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();
        }

        private void LoadCounts()
        {
            LearnedWordsCount = Preferences.Get("LearnedWordsCount", 0);
            CorrectAnswersCount = Preferences.Get("CorrectAnswersCount", 0);
            IncorrectAnswersCount = Preferences.Get("IncorrectAnswersCount", 0);
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Preferences.Set("LearnedWordsCount", LearnedWordsCount);
            Preferences.Set("CorrectAnswersCount", CorrectAnswersCount);
            Preferences.Set("IncorrectAnswersCount", IncorrectAnswersCount);
        }
    }
}
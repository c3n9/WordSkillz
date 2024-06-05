using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WordSkillz.Models;
using WordSkillz.Models.Metadata;
using WordSkillz.Pages;
using WordSkillz.Tools;

namespace WordSkillz
{
    public partial class App : Application
    {
        public static User loggedUser;
        public App()
        {
            InitializeComponent();
            RegistrateDescriptors();
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new LoadingPage();
        }

        private void RegistrateDescriptors()
        {
            AddDescriptor<User, UserMetadata>();
        }

        private void AddDescriptor<T1, T2>()
        {
            var provider = new AssociatedMetadataTypeTypeDescriptionProvider(typeof(T1), typeof(T2));
            TypeDescriptor.AddProviderTransparent(provider, typeof(T1));
        }
    }
}
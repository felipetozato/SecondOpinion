using SecondOpinion.Views.Launch;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace SecondOpinion {

    public partial class XamApplication : Application {
        public XamApplication () {
            InitializeComponent();

            AppCenter.Start("android=f5528420-5065-4e4d-b4be-73a423f1569a;" +
                  "ios=bca33005-cfef-486e-ae2b-5237cd93a727",
                  typeof(Analytics) , typeof(Crashes));

            var realmConfiguration = new Realms.RealmConfiguration();
            realmConfiguration.ShouldDeleteIfMigrationNeeded = true;
            Realms.RealmConfiguration.DefaultConfiguration = realmConfiguration;

            MainPage = new LaunchScreen();
        }

        protected override void OnStart () {
            // Handle when your app starts
        }

        protected override void OnSleep () {
            // Handle when your app sleeps
        }

        protected override void OnResume () {
            // Handle when your app resumes
        }
    }
}

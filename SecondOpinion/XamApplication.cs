using SecondOpinion.Views.Launch;
using Xamarin.Forms;

namespace SecondOpinion {

    public partial class XamApplication : Application {
        public XamApplication () {
            InitializeComponent();

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

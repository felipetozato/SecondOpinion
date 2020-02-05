using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using SecondOpinion.ViewModels;
using SecondOpinion.Views.Launch;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SecondOpinion.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/SplashTheme", MainLauncher = true)]
    public class SplashActivity : AppCompatActivity {

        public static string FolderPath { get; private set; }
        public static SplashActivity Instance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Forms.Init(this , savedInstanceState);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            Instance = this;

            SetContentView(Resource.Layout.activity_splash);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            this.SupportActionBar.Title = "Chats";

            FolderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));
            Android.Support.V4.App.Fragment mainPage = new LaunchScreen().CreateSupportFragment(this);
            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.container, mainPage)
                .Commit();
        }


        protected override void OnResume () {
            base.OnResume();
        }
    }
}

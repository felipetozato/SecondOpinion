using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Plugin.CurrentActivity;
using SecondOpinion.ViewModels;
using SecondOpinion.Views.Launch;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SecondOpinion.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MasterDetailTheme" , MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : FormsAppCompatActivity {

        public static string FolderPath { get; private set; }
        public static SplashActivity Instance;

        private const string SYNC_FUSION_KEY = "MjA2MjU3QDMxMzcyZTM0MmUzME5sK2c3QmU3YngvbVliZElSZUJhbzIzcXhYcDNyWGJDb051QlVQa0Q0Rk09";

        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this , savedInstanceState);

            AppInitializerViewModel.Initialize();

            CrossCurrentActivity.Current.Init(Application);

            AppDomain.CurrentDomain.UnhandledException += (sender , e) => {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            };

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SYNC_FUSION_KEY);

            Forms.Init(this , savedInstanceState);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            Instance = this;
            FolderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));

            LoadApplication(new XamApplication());
        }


        protected override void OnResume () {
            base.OnResume();
        }
    }
}

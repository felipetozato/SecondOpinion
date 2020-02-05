using System;

using Android.App;
using Android.OS;
using Android.Runtime;

using Plugin.CurrentActivity;
using SecondOpinion.ViewModels;
using Xamarin.Forms;

namespace SecondOpinion.Droid
{
    //You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Android.App.Application, Android.App.Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
        : base(handle, transer)
        {
        }

        private const string SYNC_FUSION_KEY = "MTgxOTU2QDMxMzcyZTMzMmUzMG9STEtRbU1tcU92TjViUVNjdWNXZ01lcGpBZDI4aWFieXJUbnpETGU2U1E9";
        

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            AppInitializerViewModel.Initialize();

            CrossCurrentActivity.Current.Init(this);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            };

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SYNC_FUSION_KEY);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {

        }

        public void OnActivityPaused(Activity activity)
        {

        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {

        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {

        }
    }
}

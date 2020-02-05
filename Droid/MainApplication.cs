//using System;

//using Android.App;
//using Android.OS;
//using Android.Runtime;

//using Plugin.CurrentActivity;
//using SecondOpinion.ViewModels;
//using SecondOpinion.Views.Launch;
//using Xamarin.Forms;

//namespace SecondOpinion.Droid
//{
//    //You can specify additional application information in this attribute
//    [Application]
//    public class MainApplication : Android.App.Application, Android.App.Application.IActivityLifecycleCallbacks
//    {
//        public MainApplication(IntPtr handle, JniHandleOwnership transer)
//        : base(handle, transer)
//        {
//        }

//        private const string SYNC_FUSION_KEY = "MjA2MjU3QDMxMzcyZTM0MmUzME5sK2c3QmU3YngvbVliZElSZUJhbzIzcXhYcDNyWGJDb051QlVQa0Q0Rk09";
        

//        public override void OnCreate()
//        {
//            base.OnCreate();
//            RegisterActivityLifecycleCallbacks(this);
            

//        }

//        public override void OnTerminate()
//        {
//            base.OnTerminate();
//            UnregisterActivityLifecycleCallbacks(this);
//        }

//        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
//        {
//            CrossCurrentActivity.Current.Activity = activity;
//        }

//        public void OnActivityDestroyed(Activity activity)
//        {

//        }

//        public void OnActivityPaused(Activity activity)
//        {

//        }

//        public void OnActivityResumed(Activity activity)
//        {
//            CrossCurrentActivity.Current.Activity = activity;
//        }

//        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
//        {

//        }

//        public void OnActivityStarted(Activity activity)
//        {
//            CrossCurrentActivity.Current.Activity = activity;
//        }

//        public void OnActivityStopped(Activity activity)
//        {

//        }
//    }
//}

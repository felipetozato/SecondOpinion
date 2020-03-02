using Foundation;
using UIKit;
using System.Threading.Tasks;
using SecondOpinion.iOS.Views;
using Xamarin.Forms;
using SecondOpinion.ViewModels;
using SecondOpinion.Views.Launch;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace SecondOpinion.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        private const string SYNC_FUSION_KEY = "MjA2MjU3QDMxMzcyZTM0MmUzME5sK2c3QmU3YngvbVliZElSZUJhbzIzcXhYcDNyWGJDb051QlVQa0Q0Rk09";


        private AppInitializerViewModel viewModel;

		public override bool WillFinishLaunching (UIApplication application , NSDictionary launchOptions) {
            viewModel = new AppInitializerViewModel();
            return true;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions) {
            AppCenter.Start("bca33005-cfef-486e-ae2b-5237cd93a727" ,
                   typeof(Analytics) , typeof(Crashes));

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SYNC_FUSION_KEY);

            new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();

            Forms.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            KeyboardOverlapRenderer.Init();
            AppInitializerViewModel.Initialize();
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            };

            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var viewController = new LaunchScreen().CreateViewController();
            SetAsMainScreen(viewController);
            return true;
        }

        private void SetAsMainScreen(UIViewController viewController) {
            this.Window.RootViewController = viewController;
            this.Window.MakeKeyAndVisible();
            this.Window.SetNeedsLayout();
            this.Window.LayoutIfNeeded();
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

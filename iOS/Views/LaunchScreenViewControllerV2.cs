using System;

using UIKit;
//using SecondOpinion.Views;
using SecondOpinion.Views.Login;
using Xamarin.Forms;
using SecondOpinion.Views;

namespace SecondOpinion.iOS.Views {
    public partial class LaunchScreenViewControllerV2 : BaseViewController<AppInitializerViewModel> {
        public LaunchScreenViewControllerV2 () : base("LaunchScreenViewControllerV2" , null) {
        }

        public override void ViewDidLoad () {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override async void ViewDidAppear (bool animated) {
            base.ViewDidAppear(animated);
            var task = await ViewModel.ShouldGoToLogin();
            if (task) {
                var initialVC = new LoginPage().CreateViewController();
                SetAsMainScreen(initialVC);
            } else {
                //var storyboard = UIStoryboard.FromName("Main", null);
                //var initialVC = storyboard.InstantiateInitialViewController();
                var initialVC = new MainTabbedPage().CreateViewController();
                SetAsMainScreen(initialVC);
            }
        }

        private void SetAsMainScreen(UIViewController viewController) {
            this.PresentViewController(viewController , true , null);
        }

        public override void DidReceiveMemoryWarning () {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


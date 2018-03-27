using System;
using SecondOpinion.ViewModels;
using ReactiveUI;
using UIKit;

namespace SecondOpinion.iOS.Login
{
    public partial class LoginViewController : Views.BaseViewController<LoginViewModel> {

        public LoginViewController(IntPtr ptr) : base(ptr) {}

        public LoginViewController() : base("LoginViewController", null) {
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            ShouldDispose(this.Bind(ViewModel, vm => vm.EmailAddress, view => view.email.Text),
                          this.Bind(ViewModel, vm => vm.Password, view => view.password.Text),
                          this.BindCommand(ViewModel, vm => vm.LoginCommand, view => view.loginButton),
                          ViewModel.LoginCommand.Subscribe(OnLoginResult));
        }

        private void OnLoginResult(bool worked) {
            var storyboard = UIStoryboard.FromName("Main" , null);
            var vc = storyboard.InstantiateInitialViewController();
            if (worked) {
                PresentViewController(vc , true , null);    
            }
        }

        public override void DidReceiveMemoryWarning() {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


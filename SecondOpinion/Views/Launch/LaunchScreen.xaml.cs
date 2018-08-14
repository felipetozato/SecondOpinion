using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

using SecondOpinion.ViewModels;
using SecondOpinion.Views.Login;

namespace SecondOpinion.Views.Launch {
    public partial class LaunchScreen : ContentPageBase<AppInitializerViewModel> {
        
        private Task<bool> shouldGoToLogin;

        public LaunchScreen () {
            InitializeComponent();
            shouldGoToLogin = ViewModel.ShouldGoToLogin();
        }

        protected override void OnAppearing () {
            base.OnAppearing();
            LoadScreen();
        }

        private async Task LoadScreen() {
            var task = await shouldGoToLogin;
            if (task) {
                var initialVC = new LoginPage();
                SetAsMainScreen(initialVC);
            } else {
                var initialVC = new MainTabbedPage();
                SetAsMainScreen(initialVC);
            }
        }

        private void SetAsMainScreen(Page page) {
            Navigation.PushModalAsync(page);
        }
    }
}

using System;
using System.Collections.Generic;

using SecondOpinion.ViewModels;
using SecondOpinion.Views;
using ReactiveUI;
using System.Reactive.Disposables;

namespace SecondOpinion.Views.Login {
    public partial class LoginPage : ContentPageBase<LoginViewModel> {
        public LoginPage () {
            InitializeComponent();
            SubscribeToViewModel();
        }

        private void SubscribeToViewModel() {
            this.WhenActivated(disposables => {
                this.Bind(ViewModel, vm => vm.EmailAddress, v => v.Email).DisposeWith(SubscriptionDisposables);
                this.Bind(ViewModel, vm => vm.Password, v => v.Password).DisposeWith(SubscriptionDisposables);
                this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.LoginButton);
            });
        }
    }
}

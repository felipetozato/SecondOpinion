using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using ReactiveUI;
using SecondOpinion.Models;
using SecondOpinion.ViewModels;

using Xamarin.Forms;

namespace SecondOpinion.Views.NewChat {
    public partial class GroupDetail : ContentPageBase<CreateGroupViewModel> {

        public GroupDetail() : base() {
            InitializeComponent();
        }

        public GroupDetail (IList<UserContact> selectedUsers) : base(selectedUsers) {
            InitializeComponent();
        }

        private void SubscribeToViewModel() {
            this.WhenActivated(disposable => {
                this.Bind(ViewModel , vm => vm.GroupName , v => v.GroupNameField)
                    .DisposeWith(SubscriptionDisposables);
                this.BindCommand(ViewModel , vm => vm.CreateGroup , v => v.Create)
                    .DisposeWith(SubscriptionDisposables);
            });
        }
    }
}

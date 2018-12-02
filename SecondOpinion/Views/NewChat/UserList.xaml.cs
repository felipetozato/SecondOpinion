using System;
using System.Collections.Generic;

using Xamarin.Forms;

using SecondOpinion.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using SecondOpinion.Views.Chat;
using SecondOpinion.Models;

namespace SecondOpinion.Views.NewChat {
    public partial class UserList : ContentPageBase<UserListViewModel> {

        private INavigation parent;

        public UserList (INavigation parentNavigation) {
            parent = parentNavigation;
            InitializeComponent();
            Init();
            SubscribeToViewModel();
            ViewModel.Populate();
        }

        private void Init () {
            CancelButton.Clicked += (sender, e) => {
                Navigation.PopModalAsync(true);
            };
            ContactList.BindingContext = ViewModel.ContactList;
            ContactList.ItemTapped += HandleTapAction;
            CreateGroup.GestureRecognizers.Add(CreateGroupHandler());
        }

        private void HandleTapAction(object sender, ItemTappedEventArgs args) {
            UserListItem item = args.Item as UserListItem;
            Dialog dialog = ViewModel.GetDialog(item.Item);
            var chatPage = new ChatPage(dialog);
            parent.PushAsync(chatPage, true);
            parent.PopModalAsync();
        }

        private void SubscribeToViewModel() {
            this.WhenActivated(disposables => {
                ContactList.ItemsSource = ViewModel.ContactList;
                ViewModel.ContactList.Changed
                         .Where(list => list != null && list.NewItems.Count > 0)
                         .ObserveOn(RxApp.MainThreadScheduler)
                         .Subscribe(chatList => {
                    ContactList.ItemsSource = ViewModel.ContactList;
                         });
            });
        }

        TapGestureRecognizer CreateGroupHandler() {
            var tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) => {
                var addParticipant = new AddParticipant(parent);
                Navigation.PushAsync(addParticipant);
            };
            return tap;
        }

    }
}

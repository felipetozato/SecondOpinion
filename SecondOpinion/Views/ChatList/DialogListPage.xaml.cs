using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SecondOpinion.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using SecondOpinion.Models;
using SecondOpinion.Views.Chat;
using System.Reactive.Disposables;
using SecondOpinion.Views.NewChat;
using Microsoft.AppCenter.Crashes;

namespace SecondOpinion.Views.ChatList {
    public partial class DialogListPage : ContentPageBase<DialogListViewModel> {
        public DialogListPage () {
            InitializeComponent();
            Init();
            SubscribeToViewModel();
            this.DialogList.BindingContext = ViewModel;
        }

        void Init() {
            NewChatButton.Clicked += NewChatButton_Clicked;
            DialogList.ItemsSource = ViewModel.ChatList;
        }

        protected override void OnAppearing () {
            base.OnAppearing();
            ViewModel.Populate();
            DialogList.ItemTapped += ItemTappedHandler;
        }

        protected override void OnDisappearing () {
            base.OnDisappearing();
            DialogList.ItemTapped -= ItemTappedHandler;
        }

        void NewChatButton_Clicked (object sender , EventArgs e) {
            var newMessageScreen = new UserList(this.Navigation);
            Navigation.PushModalAsync(new NavigationPage(newMessageScreen) , true);
        }

        private void SubscribeToViewModel () => this.WhenActivated(disposables => {
            ViewModel.ChatList.Changed
                     .Where(list => list != null && list.NewItems.Count > 0)
                     .ObserveOn(RxApp.MainThreadScheduler)
                     .Subscribe(chatList => {
                             //DialogList.ItemsSource = null;
                             //DialogList.ItemsSource = chatList.NewItems;
                         });
        });

        private void ItemTappedHandler(object sender, ItemTappedEventArgs e) {
            var dialog = e.Item as Dialog;
            var chatPage = new ChatPage(dialog);
            this.Navigation.PushAsync(chatPage);
        }
    }
}

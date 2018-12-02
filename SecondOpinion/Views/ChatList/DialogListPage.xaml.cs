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

namespace SecondOpinion.Views.ChatList {
    public partial class DialogListPage : ContentPageBase<DialogListViewModel> {
        public DialogListPage () {
            InitializeComponent();
            Init();
            SubscribeToViewModel();
            ViewModel.Populate();
            this.DialogList.BindingContext = ViewModel.ChatList;
        }

        void Init() {
            NewChatButton.Clicked += NewChatButton_Clicked;
            DialogList.ItemsSource = ViewModel.ChatList;
        }

        void NewChatButton_Clicked (object sender , EventArgs e) {
            var newMessageScreen = new UserList(this.Navigation);
            Navigation.PushModalAsync(new NavigationPage(newMessageScreen) , true);
        }

        private void SubscribeToViewModel() {
            this.WhenActivated(disposables => {
                DialogList.ItemSelected += (sender, e) => {
                    var dialog = e.SelectedItem as Dialog;
                    var chatPage = new ChatPage(dialog);
                    this.Navigation.PushAsync(chatPage);
                };

                ViewModel.ChatList.Changed
                         .Where(list => list != null && list.NewItems.Count > 0)
                         .ObserveOn(RxApp.MainThreadScheduler)
                         .Subscribe(chatList => {
                    //DialogList.ItemsSource = null;
                    //DialogList.ItemsSource = chatList.NewItems;
                });
            });
        }
    }
}

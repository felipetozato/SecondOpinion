using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SecondOpinion.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using SecondOpinion.Models;
using SecondOpinion.Views.Chat;
using System.Reactive.Disposables;

namespace SecondOpinion.Views.ChatList {
    public partial class DialogListPage : ContentPageBase<DialogListViewModel> {
        public DialogListPage () {
            InitializeComponent();
            Init();
            ViewModel.Populate();
            SubscribeToViewModel();

            this.Title = "Conversas";
        }

        void Init() {
            this.WhenActivated(disposables => {
                DialogList.Events().ItemSelected
                          .Select(x => x.SelectedItem as Dialog)
                          .Subscribe(dialog => {
                    var chatPage = new ChatPage(dialog);
                    this.Navigation.PushAsync(chatPage);
                }).DisposeWith(SubscriptionDisposables);
            });
        }

        private void SubscribeToViewModel() {
            this.WhenActivated(disposables => {
                ViewModel.ChatList.Changed
                         .Where(list => list != null && list.NewItems.Count > 0)
                         .ObserveOn(RxApp.MainThreadScheduler)
                         .Subscribe(chatList => {
                    DialogList.ItemsSource = chatList.NewItems;
                });
            });
        }
    }
}

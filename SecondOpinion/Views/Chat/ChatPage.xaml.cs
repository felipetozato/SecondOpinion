using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using SecondOpinion.ViewModels;
using SecondOpinion.Models;
using ReactiveUI;
using System.Reactive.Linq;
using System.Collections.Specialized;
using System.Reactive.Disposables;

namespace SecondOpinion.Views.Chat {
    public partial class ChatPage : ContentPageBase<DialogViewModel> {
        public ChatPage (Dialog chat) : base() {
            InitializeComponent();
            ViewModel.Chat = chat;
            ViewModel.Populate();
            SubscribeToViewModel();
        }

        private void SubscribeToViewModel() {
            this.WhenActivated(disposable => {
                this.Bind(ViewModel , vm => vm.MessageText , v => v.MessageEditText.Text)
                    .DisposeWith(SubscriptionDisposables);
                this.BindCommand(ViewModel , vm => vm.SendMessageCommand , v => v.SendMessageButton)
                    .DisposeWith(SubscriptionDisposables);
                ViewModel.MessageList.Changed
                         .ObserveOn(RxApp.MainThreadScheduler)
                         .Subscribe(AddMessageToTableView);
            });
        }

        private void AddMessageToTableView (NotifyCollectionChangedEventArgs eventArgs) {
            this.MessageList.ItemsSource = ViewModel.MessageList.OrderBy(x => x.Id);
        }
    }
}

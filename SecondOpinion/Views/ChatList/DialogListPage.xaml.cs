using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SecondOpinion.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;

namespace SecondOpinion.Views.ChatList {
    public partial class DialogListPage : ContentPageBase<DialogListViewModel> {
        public DialogListPage () {
            InitializeComponent();
            ViewModel.Populate();
            SubscribeToViewModel();

            this.Title = "Conversas";
        }

        private void SubscribeToViewModel() {
            this.WhenActivated(disposables => {
                ViewModel.ChatList.Changed
                         .Where(list => list != null && list.NewItems.Count > 0)
                         .Subscribe(chatList => {
                    DialogList.ItemsSource = chatList.NewItems;
                });

            });
        }
    }
}

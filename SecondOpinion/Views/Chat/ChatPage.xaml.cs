using System;using System.Collections.Generic;using System.Linq;using Xamarin.Forms;using SecondOpinion.ViewModels;using SecondOpinion.Models;using SecondOpinion.Repositories;using ReactiveUI;using System.Reactive.Linq;using System.Collections.Specialized;using System.Reactive.Disposables;using Splat;
using SecondOpinion.Services.Api;

namespace SecondOpinion.Views.Chat {    public partial class ChatPage : ContentPageBase<DialogViewModel> {        UserLogin currentUser;        public ChatPage (Dialog chat) : base() {            InitializeComponent();            ViewModel.Chat = chat;            ViewModel.Populate();            SubscribeToViewModel();            currentUser = Locator.Current.GetService<ISettingsRepository>().GetUserLogin();            MessageList.BindingContext = ViewModel;            MessageEditText.BindingContext = ViewModel;            MessageEditText.Filter = MessageEditTextFilter;
        }        private void SubscribeToViewModel () {            this.WhenActivated(disposable => {                this.Bind(ViewModel , vm => vm.MessageText , v => v.MessageEditText.Text)                    .DisposeWith(SubscriptionDisposables);                this.BindCommand(ViewModel , vm => vm.SendMessageCommand , v => v.SendMessageButton)                    .DisposeWith(SubscriptionDisposables);                ViewModel.WhenAnyValue(vm => vm.MessageList)                         .ObserveOn(RxApp.MainThreadScheduler)                         .Subscribe(AddMessageToTableView);                ViewModel.MessageList.Changed                         .ObserveOn(RxApp.MainThreadScheduler)                         .Subscribe(AddMessageToTableView);                this.Bind(ViewModel , vm => vm.IsRefreshing , v => v.MessageList.IsRefreshing);                MessageList.Refreshing += RefreshingMethod;
            });
        }
        protected override void OnDisappearing () {
            base.OnDisappearing();            MessageList.Refreshing -= RefreshingMethod;
            ViewModel.Dispose();
        }
        private async void RefreshingMethod(object sender, EventArgs e) {
            MessageList.IsRefreshing = true;
            var filteredItems = await ApiCoordinator.GetPrivateMessages(ViewModel.Chat.Id);
            ViewModel.MessageList.Clear();
            ViewModel.MessageList.AddRange(filteredItems.Items);
            MessageList.IsRefreshing = false;
        }        private void AddMessageToTableView(IList<Message> list) {            this.MessageList.ItemsSource = list.OrderBy(x => x.Id)                .Select(message => new {MessageBody = message.MessageBody, MessageColor = GetColor(message), MessageSender = message.SenderName});        }                private void AddMessageToTableView (NotifyCollectionChangedEventArgs eventArgs) {            this.MessageList.ItemsSource = ViewModel.MessageList.OrderBy(x => x.Id)                .Select(message => new { MessageBody = message.MessageBody , MessageColor = GetColor(message), MessageSender = message.SenderName });        }        private Color GetColor(Message message) {            return currentUser.UserId.ToString() == message.SenderId ? ColorUtils.MessageBlue : ColorUtils.MessageGreen;        }        private bool MessageEditTextFilter(string search, object item) {
            string text = item.ToString().ToLower();
            if (search?.Length >= 2 && text != null) {
                return text.StartsWith(search.ToLower());
            }
            return false;
        }    }}
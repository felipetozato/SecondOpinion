using System;
using System.Threading.Tasks;
using SecondOpinion.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using SecondOpinion.Services.Api;
using System.Collections.Generic;
using System.Linq;
using Splat;
using SecondOpinion.Repositories;
using System.Windows.Input;
using Xamarin.Forms;
using SecondOpinion.Repositories.Intefaces;
using System.Timers;
using System.Reactive.Concurrency;

namespace SecondOpinion.ViewModels
{
    public class DialogViewModel : BaseViewModel {

        /// <summary>
        /// The user to chat with
        /// </summary>
        private Dialog _Dialog;
        public Dialog Chat {
            get => _Dialog;
            set {
                _Dialog = value;
                DialogId = value.Id;
            }
        }

        private String DialogId = "";

        /// <summary>
        /// Gets the message list.
        /// </summary>
        /// <value>The message list.</value>
        public ReactiveList<Message> MessageList {
            get;
            private set;
        }

        /// <summary>
        /// Gets the suggestion list
        /// </summary>
        public ReactiveList<String> SuggestionList {
            get;
            private set;
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing {
            get { return _isRefreshing; }
            set {
                _isRefreshing = value;
                this.RaiseAndSetIfChanged(ref _isRefreshing , value);
            }
        }

        public ICommand RefreshCommand {
            get {
                return new Command(async () => {
                    IsRefreshing = true;

                    var filteredItems = await ApiCoordinator.GetPrivateMessages(Chat.Id);
                    MessageList.Clear();
                    MessageList.AddRange(filteredItems.Items);

                    IsRefreshing = false;
                });
            }
        }

        public UserLogin CurrentUserLogin {
            get;
            private set;
        }

        private string _MessageText;
        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <value>The message text.</value>
        public string MessageText {
            get => _MessageText;
            set => this.RaiseAndSetIfChanged(ref _MessageText, value);
        }

        /// <summary>
        /// Gets or sets the send message command.
        /// </summary>
        /// <value>The send message command.</value>
        public ReactiveCommand<Unit, bool> SendMessageCommand {
            get;
            set;
        }

        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SecondOpinion.ViewModels.PrivateChatViewModel"/> class.
        /// </summary>
        /// <param name="userToChat">User to chat.</param>
        public DialogViewModel() {
            MessageList = new ReactiveList<Message>();
            SuggestionList = new ReactiveList<string>();
            SendMessageCommand = ReactiveCommand.CreateFromObservable<bool>(SendMessage);
            SendMessageCommand.Subscribe(worked => {
                MessageText = string.Empty;
            });
        }

        public void Populate() {
            // Load existing messages
            GetMessages(Chat.Id);
            CurrentUserLogin = Locator.CurrentMutable.GetService<ISettingsRepository>().GetUserLogin();
            GetSuggestionList();
            timer = new Timer() {
                Interval = 4000,
                AutoReset = false,
                Enabled = false
            };
            timer.Elapsed += ElapsedTime;

        }

        private void GetMessages(string dialogId) {
            Observable.FromAsync(() => ApiCoordinator.GetPrivateMessages(dialogId))
                      .Where(page => page.Items.Count > 0)
                      .Subscribe(page => {
                          MessageList.AddRange(page.Items);
                          timer.Enabled = true;
                          timer.Start();
            });
        }

        private IObservable<bool> SendMessage() {
            if (string.IsNullOrEmpty(_MessageText)) {
                return Observable.Return(false);
            }
            var messageBody = Chat.Id != null ?
                CreateChatDialogMessage(Chat.Id, CurrentUserLogin.userInfo.Name, _MessageText) :
                Create1o1Message(Chat.OccupantsIds.First(id => this.Chat.UserId != id), CurrentUserLogin.userInfo.Name, _MessageText);

            return SendMessage(messageBody);
        }

        private Message Create1o1Message(long recipientId, string senderId, string message) {
            return new Message() {
                ToUserId = recipientId,
                MessageBody = message,
                SenderId = senderId
            };
        }

        private Message CreateChatDialogMessage(String chatDialogId, string senderId, string message) {
            return new Message() {
                ChatDialogId = chatDialogId ,
                MessageBody = message,
                SenderId = senderId
            };
        }

        private IObservable<bool> SendMessage(Message message) {
            return Observable.FromAsync(async () => {
                var result = await ApiCoordinator.SendPrivateMessage(message);
                Chat.Id = result.ChatDialogId;
                result.Dialog = Chat;
                MessageList.Add(result);
                return true;
            });
        }

        private void GetSuggestionList() {
            var repository = Locator.CurrentMutable.GetService<ISuggestionRepository>();
            Observable.FromAsync(() => repository.GetSuggestionList(CurrentUserLogin.userInfo.Name))
                .Subscribe(onNext: list => {
                    SuggestionList.AddRange(list);
                }, onError: ex => {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                });
        }

        override public void Dispose () {
            base.Dispose();
            if (timer != null) {
                timer.Close();
                timer.Dispose();
                timer.Elapsed -= ElapsedTime;
            }
        }

        private async void ElapsedTime(object sender, ElapsedEventArgs args) {
            System.Diagnostics.Debug.WriteLine("Elasped TIME!!!!");
            var page = await ApiCoordinator.GetPrivateMessages(DialogId);
            var newItems = page.Items.Except(MessageList);
            if (newItems.Count() > 0) {
                MessageList.AddRange(newItems);
            }
            timer.Start();
            //Observable.FromAsync(() => ApiCoordinator.GetPrivateMessages(Chat.Id))
            //          .Select(page => {
            //              return page.Items.Except(MessageList);
            //          })
            //          .Subscribe(list => {
            //              timer.Start();
            //              if (list.Count() > 0) {
            //                  MessageList.AddRange(list);
            //              }
            //          });
        }
    }
}
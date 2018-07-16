using System;
using System.Threading.Tasks;
using SecondOpinion.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using SecondOpinion.Services.Api;
using System.Collections.Generic;
using System.Linq;

namespace SecondOpinion.ViewModels
{
    public class DialogViewModel : BaseViewModel {

        /// <summary>
        /// The user to chat with
        /// </summary>
        public Dialog Chat {
            get;
            set;
        }

        /// <summary>
        /// Gets the message list.
        /// </summary>
        /// <value>The message list.</value>
        public ReactiveList<Message> MessageList {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SecondOpinion.ViewModels.PrivateChatViewModel"/> class.
        /// </summary>
        /// <param name="userToChat">User to chat.</param>
        public DialogViewModel() {
            MessageList = new ReactiveList<Message>();
            SendMessageCommand = ReactiveCommand.CreateFromObservable<bool>(SendMessage);
        }

        public void Populate() {
            // Load existing messages
            GetMessages(Chat.Id);
        }

        private void GetMessages(string dialogId) {
            Observable.FromAsync(() => ApiCoordinator.GetPrivateMessages(dialogId))
                      .Where(page => page.Items.Count > 0)
                      .Subscribe(page => {
                MessageList.AddRange(page.Items);
            });
        }

        private IObservable<bool> SendMessage() {
            if (string.IsNullOrEmpty(_MessageText)) {
                return Observable.Return(false);
            }
            var messageBody = Chat.Id != null ?
                CreateChatDialogMessage(Chat.Id, _MessageText) :
                                  Create1o1Message(Chat.OccupantsIds.First(id => this.Chat.UserId != id) , _MessageText);
            
            return SendMessage(messageBody);
        }

        private Message Create1o1Message(long recipientId , string message) {
            return new Message() {
                ToUserId = recipientId ,
                MessageBody = message
            };
        }

        private Message CreateChatDialogMessage(String chatDialogId, string message) {
            return new Message() {
                ChatDialogId = chatDialogId ,
                MessageBody = message
            };
        }

        private IObservable<bool> SendMessage(Message message) {
            return Observable.FromAsync(async () => {
                var result = await ApiCoordinator.SendPrivateMessage(message);
                result.Dialog = Chat;
                MessageList.Add(result);
                return true;
            });
        }
    }
}

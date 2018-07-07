using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using Foundation;
using ReactiveUI;
using SecondOpinion.Models;
using SecondOpinion.ViewModels;

using UIKit;

namespace SecondOpinion.iOS.Chat
{
    public partial class DialogViewController : Views.BaseViewController<DialogViewModel>, IUITableViewDataSource, IUITableViewDelegate, IUITextFieldDelegate {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SecondOpinion.iOS.Chat.ChatViewController"/> class.
        /// Constructor when initializing over Storyboard
        /// </summary>
        /// <param name="ptr">Ptr.</param>
        public DialogViewController (IntPtr ptr) : base(ptr) {

        }

        public void SetChat (Dialog chat) {
            ViewModel.Chat = chat;
        }

        public override void LoadView () {
            base.LoadView();

            ViewModel.Populate();

            this.Title = ViewModel.Chat.Name;
        }

        public override void ViewDidLoad () {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            MessageList.Delegate = this;
            MessageList.DataSource = this;
            MessageEditText.Delegate = this;

            SubscribeToViewModel();
        }

        private void SubscribeToViewModel () {
            ShouldDispose(
                this.Bind(ViewModel , vm => vm.MessageText , v => v.MessageEditText.Text) ,
                this.BindCommand(ViewModel , vm => vm.SendMessageCommand , v => v.SendMessageButton) ,
                ViewModel.MessageList.Changed.ObserveOn(RxApp.MainThreadScheduler)
                .Do(AddMessageToTableView)
                .Subscribe(CleanOldMessage)
            );
        }

        public nint RowsInSection (UITableView tableView , nint section) {
            return ViewModel.MessageList.Count;
        }

        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections (UITableView tableView) {
            return 1;
        }

        public UITableViewCell GetCell (UITableView tableView , NSIndexPath indexPath) {
            var cell = MessageList.DequeueReusableCell("MessageCell" , indexPath);
            cell.TextLabel.Text = ViewModel.MessageList[indexPath.Row].MessageBody;

            return cell;
        }

        [Export("textFieldShouldReturn:")]
        public bool ShouldReturn (UITextField textField) {
            ViewModel.SendMessageCommand.Execute();
            return true;
        }

        private void AddMessageToTableView (NotifyCollectionChangedEventArgs eventArgs) {
            if (eventArgs.Action == NotifyCollectionChangedAction.Reset) {
                MessageList.ReloadData();
            } else {
                MessageList.BeginUpdates();
                MessageList.InsertRows(
                    new NSIndexPath[] { NSIndexPath.FromRowSection(ViewModel.MessageList.Count - 1 , 0) } ,
                    UITableViewRowAnimation.Bottom);
                MessageList.EndUpdates();
            }

        }

        private void CleanOldMessage (NotifyCollectionChangedEventArgs eventArgs) {
            MessageEditText.Text = "";
            MessageList.ScrollToRow(NSIndexPath.FromItemSection(ViewModel.MessageList.Count - 1, 0),
                                    UITableViewScrollPosition.Top, true);
        }
    }
}


using System;
using UIKit;
using ReactiveUI;
using SecondOpinion.ViewModels;
using System.Reactive.Linq;
using Foundation;
using SecondOpinion.Models;
using CoreAnimation;
using SecondOpinion.iOS.Views;
using SecondOpinion.iOS.Chat;

namespace SecondOpinion.iOS.ChatList
{
    public partial class DialogListViewController : Views.BaseTableViewController<DialogListViewModel>
    {

        UIBarButtonItem newMessageButton;

        public DialogListViewController() : base("ChatListViewController", null) {
            ViewModel = new DialogListViewModel();
        }

        public DialogListViewController(IntPtr ptr) : base(ptr) {
            ViewModel = new DialogListViewModel();
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            ViewModel.Populate();
            SubscribeToViewModel();
            // Perform any additional setup after loading the view, typically from a nib.
            newMessageButton = new UIBarButtonItem(UIBarButtonSystemItem.Compose);
            newMessageButton.Clicked += GoToNewMessage;
            this.NavigationItem.RightBarButtonItem = newMessageButton;
            this.NavigationItem.Title = "Chats";
        }

        public override void DidReceiveMemoryWarning() {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override nint NumberOfSections(UITableView tableView) {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section) {
            return ViewModel.ChatList.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath) {
            var cell = tableView.DequeueReusableCell("ChatListCell");
            cell.TextLabel.Text = ViewModel.ChatList[indexPath.Row].Name;
            cell.DetailTextLabel.Text = ViewModel.ChatList[indexPath.Row].LastMessage;
            return cell;
        }

        public override void RowSelected (UITableView tableView , NSIndexPath indexPath) {
            var chat = ViewModel.ChatList[indexPath.Row];
            chat.User = UserContact.Create(chat.UserId , chat.Name);
            var storyboard = UIStoryboard.FromName(StoryboardNames.CHAT, null);
            var viewController = storyboard.InstantiateInitialViewController() as DialogViewController;
            viewController.SetChat(chat);
            this.NavigationController.PushViewController(viewController, true);

        }

        private void GoToNewMessage(object sender, EventArgs args) {
            var storyBoard = UIStoryboard.FromName("UserList" , null);
            var viewController = storyBoard.InstantiateInitialViewController();

            this.NavigationController.PushViewController(viewController, true);
        }

        private void SubscribeToViewModel() {
            ShouldDispose(ViewModel.ChatList.Changed
                          .Where(list => list != null && list.NewItems.Count > 0)
                          .Subscribe(chatList => {
                
                NSIndexPath[] indexes = new NSIndexPath[chatList.NewItems.Count];
                for (int i = chatList.NewStartingIndex; i < chatList.NewItems.Count; i++) {
                    indexes[i] = NSIndexPath.FromRowSection(i, 0);
                }
                ListView.BeginUpdates();
                ListView.InsertRows(indexes, UITableViewRowAnimation.Fade);
                ListView.EndUpdates();
            }));
        }
    }
}


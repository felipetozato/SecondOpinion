using Foundation;
using System;
using UIKit;
using ReactiveUI;
using SecondOpinion.ViewModels;
using SecondOpinion.Models;
using System.Reactive.Linq;

namespace SecondOpinion.iOS.Views
{
    public partial class UserListViewController : BaseTableViewController<UserListViewModel>
    {
        partial void OnCancelClick(UIBarButtonItem sender) {
            this.NavigationController.PopViewController(true);
        }

        public UserListViewController (IntPtr handle) : base (handle)
        {
            ViewModel = new UserListViewModel();
            ViewModel.Populate().ConfigureAwait(false);
        }

        public override void ViewDidLoad() {
            base.ViewDidLoad();

            NavigationItem.HidesBackButton = true;
            UserTable.DataSource = this;
            UserTable.Delegate = this;
            SubscribeToViewModel();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
            var cell = tableView.DequeueReusableCell("UserListCell", indexPath);

            var user = ViewModel.ContactList[indexPath.Row];
            cell.TextLabel.Text = user.Name;
            cell.DetailTextLabel.Text = user.Email;

            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section) {
            return ViewModel.ContactList.Count;
        }

        /// <summary>
        /// Rows the selected.
        /// </summary>
        /// <param name="tableView">Table view.</param>
        /// <param name="indexPath">Index path.</param>
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
            var user = ViewModel.ContactList[indexPath.Row];

            var storyBoard = UIStoryboard.FromName("Chat", null);
            var viewController = storyBoard.InstantiateInitialViewController() as DialogViewController;
            var chat = new Dialog();
            chat.User = user;
            chat.Name = user.Name;
            viewController.SetChat(chat);
            NavigationController.SetViewControllers(new UIViewController[] {NavigationController.ViewControllers[0], viewController}, true);
        }

        private void SubscribeToViewModel() {
            ShouldDispose(
                ViewModel.ContactList.Changed
                .Where(list => list != null && list.NewItems.Count > 0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(newList => {
                    NSIndexPath[] indexes = new NSIndexPath[newList.NewItems.Count];
                    for (int i = newList.NewStartingIndex; i < newList.NewItems.Count; i++) {
                        indexes[i] = NSIndexPath.FromRowSection(i , 0);
                    }
                    UserTable.BeginUpdates();
                    UserTable.InsertRows(indexes , UITableViewRowAnimation.Fade);
                    UserTable.EndUpdates();
                })
            );
        }
    }
}
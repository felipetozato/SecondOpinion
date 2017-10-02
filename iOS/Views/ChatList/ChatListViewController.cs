using System;
using UIKit;
using ReactiveUI;
using SecondOpinion.ViewModels;

namespace SecondOpinion.iOS.Views.ChatList
{
    public partial class ChatListViewController : ReactiveTableViewController<ChatListViewModel>
    {
        public ChatListViewController() : base("ChatListViewController", null)
        {
            ViewModel = new ChatListViewModel();
        }

        public ChatListViewController(IntPtr ptr) : base(ptr) {
            ViewModel = new ChatListViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return 5;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("ChatListCell");

            return cell;
        }
    }
}


// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace SecondOpinion.iOS.UserList
{
	public partial class UserTableViewCell : UITableViewCell
	{

		public UserTableViewCell (IntPtr handle) : base (handle){
            InitView();
		}

        public void SetName(string name) {
            this.Name.Text = name;
        }

        public void SetPhoto(UIImage image) {
            this.ImageView.Image = image;
        }

        private void InitView () {
            NSArray array = NSBundle.MainBundle.LoadNib("UserListCell" , this , null);
            var view = array.GetItem<UIView>(0);
            this.AddSubview(view);
            view.Frame = this.Bounds;
            view.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
        }
	}
}

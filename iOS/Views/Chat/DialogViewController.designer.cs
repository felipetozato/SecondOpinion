// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SecondOpinion.iOS.Views
{
    [Register ("ChatViewController")]
    partial class DialogViewController
    {
        [Outlet]
        UIKit.UITextField MessageEditText { get; set; }


        [Outlet]
        UIKit.UITableView MessageList { get; set; }


        [Outlet]
        UIKit.UIButton SendMessageButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MessageEditText != null) {
                MessageEditText.Dispose ();
                MessageEditText = null;
            }

            if (MessageList != null) {
                MessageList.Dispose ();
                MessageList = null;
            }

            if (SendMessageButton != null) {
                SendMessageButton.Dispose ();
                SendMessageButton = null;
            }
        }
    }
}
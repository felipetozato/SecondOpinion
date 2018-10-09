// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SecondOpinion.iOS.ChatList
{
    [Register ("ChatListViewController")]
    partial class DialogListViewController
    {
        [Outlet]
        UIKit.UITableView ListView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ListView != null) {
                ListView.Dispose ();
                ListView = null;
            }
        }
    }
}
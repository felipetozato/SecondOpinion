// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SecondOpinion.iOS.UserList
{
    [Register ("UserListViewController")]
    partial class UserListViewController
    {
        [Outlet]
        UIKit.UIView CreateGroup { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CancelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView UserTable { get; set; }


        [Action ("OnCancelClick:")]
        partial void OnCancelClick (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (CancelButton != null) {
                CancelButton.Dispose ();
                CancelButton = null;
            }

            if (CreateGroup != null) {
                CreateGroup.Dispose ();
                CreateGroup = null;
            }

            if (UserTable != null) {
                UserTable.Dispose ();
                UserTable = null;
            }
        }
    }
}
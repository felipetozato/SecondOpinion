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
    [Register ("GroupDetailViewController")]
    partial class GroupDetailViewController
    {
        [Outlet]
        UIKit.UIBarButtonItem CreateGroupButton { get; set; }


        [Outlet]
        UIKit.UIImageView GroupImage { get; set; }


        [Outlet]
        UIKit.UITextField GroupSubject { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CreateGroupButton != null) {
                CreateGroupButton.Dispose ();
                CreateGroupButton = null;
            }

            if (GroupImage != null) {
                GroupImage.Dispose ();
                GroupImage = null;
            }

            if (GroupSubject != null) {
                GroupSubject.Dispose ();
                GroupSubject = null;
            }
        }
    }
}
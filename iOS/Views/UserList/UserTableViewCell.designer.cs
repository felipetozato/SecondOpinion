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
    [Register ("UserTableViewCell")]
    partial class UserTableViewCell
    {
        [Outlet]
        UIKit.UILabel Name { get; set; }


        [Outlet]
        UIKit.UIImageView Photo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Name != null) {
                Name.Dispose ();
                Name = null;
            }

            if (Photo != null) {
                Photo.Dispose ();
                Photo = null;
            }
        }
    }
}
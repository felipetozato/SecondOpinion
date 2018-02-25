using System;
using SecondOpinion.Models;
using SecondOpinion.Services;
using UIKit;

namespace SecondOpinion.iOS.Services
{
    public class IOSRouter : IRouter
    {

        private UIViewController _currentController;
        public UIViewController CurrentController {
            get => _currentController;
            set => _currentController = value;
        }

        /// <summary>
        /// Opens the contact list.
        /// </summary>
        public void OpenContactList() {

            //var storyBoard = UIStoryboard.FromName("UserList", null);
            //var viewController = storyBoard.InstantiateInitialViewController();
            //if (CurrentController != null) {
            //    CurrentController.PresentViewController(viewController, true, null);
            //}
            //CurrentController = viewController;
        }

        public void OpenPrivateChat(UserContact user) {

            //var storyBoard = UIStoryboard.FromName("Chat", null);
            //var viewController = storyBoard.InstantiateInitialViewController() as ChatViewController;
            //viewController.SetUser(user);

            //CurrentController.NavigationController.PushViewController(viewController, true);
            //CurrentController = viewController;
        }
    }
}

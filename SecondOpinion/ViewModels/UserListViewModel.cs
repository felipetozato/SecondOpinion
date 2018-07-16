using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SecondOpinion.Models;
using SecondOpinion.Services.Api;
using SecondOpinion.Repositories;
using Splat;

namespace SecondOpinion.ViewModels
{
    public class UserListViewModel : BaseViewModel {

        public ReactiveList<UserListItem> ContactList {
            get;
            private set;
        }

        public ReactiveCommand<int , Unit> SelectItem;

        public ReactiveCommand<int , Unit> DeselectItem;

        public UserListViewModel () : base() {
            ContactList = new ReactiveList<UserListItem>();
            SelectItem = ReactiveCommand.Create<int>(HandleSelectItem , null , null);
            DeselectItem = ReactiveCommand.Create<int>(HandleDeselectItem , null , null);
        }

        public void Populate() {
            GetUsersFromServer();
        }

        public IList<UserContact> GetSelectedUsers() => ContactList.Where( item => item.Selected == true).Select(item => item.Item).ToList();

        private void GetUsersFromServer() {
            try {
                Locator.Current.GetService<IUserContactRepository>().GetAllUserContact()
                       .Subscribe(users => {
                    ContactList.AddRange(users.Select(UserListItem.Create));
                    System.Diagnostics.Debug.WriteLine("WORKED");
                });
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void HandleDeselectItem (int index) {
            ContactList[index].Selected = false;
        }

        private void HandleSelectItem (int index) {
            ContactList[index].Selected = true;
        }
    }

    public class UserListItem {

        public bool Selected {
            get;
            set;
        }

        public UserContact Item {
            get;
            set;
        }

        private UserListItem (UserContact item) {
            Item = item;
        }

        public static UserListItem Create (UserContact item) => new UserListItem(item);
    }
}

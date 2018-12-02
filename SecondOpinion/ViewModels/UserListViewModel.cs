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
using Xamarin.Forms;

namespace SecondOpinion.ViewModels
{
    public class UserListViewModel : BaseViewModel {

        public ReactiveList<UserListItem> ContactList {
            get;
            private set;
        }

        private IChatRepository chatRepository;

        private UserLogin currentUser;

        public UserListViewModel () : base() {
            ContactList = new ReactiveList<UserListItem>();
            chatRepository = Locator.Current.GetService<IChatRepository>();

            currentUser = Locator.Current.GetService<ISettingsRepository>().GetUserLogin();
        }

        public void Populate() {
            GetUsersFromServer();
        }

        public IList<UserContact> GetSelectedUsers() => 
        ContactList.Where( item => item.Selected == true).Select(item => item.Item).ToList();

        public Dialog GetDialog(UserContact contact) {
            var dialog = chatRepository.GetDialog(contact);
            if (dialog == null) {
                dialog = new Dialog(new long[] { currentUser.UserId , contact.Id } , currentUser.UserId , 
                                    Dialog.ChatType.PrivateChat, contact.Name);
            }
            return dialog;
        }

        private void GetUsersFromServer() {
            try {
                Locator.Current.GetService<IUserContactRepository>().GetAllUserContact().Subscribe(users => {
                var items = users.Where(user => user.Id != currentUser.UserId).Select(UserListItem.Create);
                    var filteredItems =  items.Except(ContactList);
                    ContactList.AddRange(filteredItems);
                    System.Diagnostics.Debug.WriteLine("WORKED");
                });
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }

    public class UserListItem : ReactiveObject {

        private bool _selected;
        public bool Selected {
            get => _selected;
            private set => this.RaiseAndSetIfChanged(ref _selected , value);
        }

        public UserContact Item {
            get;
            set;
        }

        private UserListItem (UserContact item) {
            Item = item;
        }

        public static UserListItem Create (UserContact item) => new UserListItem(item);

        public void Select() {
            Selected = true;
        }

        public void DeSelect() {
            Selected = false;
        }

        public override bool Equals (object obj) {
            if (obj is UserListItem listItem) {
                return this.Item.Equals(listItem.Item);
            } else {
                return false;
            }
        }
    }
}

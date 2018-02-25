using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SecondOpinion.Models;
using SecondOpinion.Services.Api;

namespace SecondOpinion.ViewModels
{
    public class UserListViewModel : BaseViewModel
    {
        
        public ReactiveList<UserContact> ContactList {
            get;
            private set;
        }

        public UserListViewModel() : base() {
            ContactList = new ReactiveList<UserContact>();
        }

        public async Task Populate() {
            await GetUsersFromServer();
        }

        private async Task GetUsersFromServer() {
            try {
                var result = await Task.Run( () => {
                    var userLogin = UserSettings.GetUserLogin();
                    return ApiCoordinator.GetAllContacts();

                });
                ContactList.AddRange(result.Items);
                System.Diagnostics.Debug.WriteLine("WORKED");
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

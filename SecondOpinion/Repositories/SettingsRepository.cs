using System;
using System.Threading.Tasks;
using System.Linq;
using Realms;
using SecondOpinion.Models;

namespace SecondOpinion.Repositories
{
    /// <summary>
    /// Settings repository.
    /// </summary>
    public class SettingsRepository : ISettingsRepository
    {

        //TODO Instead of getting a realm instance every time. Create a thread just to run realm and them create a single object there.

        public SettingsRepository() {
        }

        /// <summary>
        /// Gets the user login.
        /// </summary>
        /// <returns>The user login.</returns>
        public UserLogin GetUserLogin() {
            var realm = Realm.GetInstance();
            var users = realm.All<UserLogin>();
            return (users.Count() > 0) ? users.First() : null;
        }

        public Task InvalidateUserLogin (UserLogin userLogin) {
            userLogin.Realm.Write(() => {
                userLogin.Realm.Remove(userLogin);
            }); 
            return Task.CompletedTask;
        }

        /// <summary>
        /// Saves the user login.
        /// </summary>
        /// <param name="userLogin">User login.</param>
        public Task SaveOrUpdateUserLogin(UserLogin userLogin) {
            var realm = Realm.GetInstance();
            return realm.WriteAsync(realmTemp => {
                realmTemp.Add(userLogin , true);
            });
        }

        /// <summary>
        /// Deletes the token.
        /// </summary>
        /// <returns>The token.</returns>
        public Task DeleteToken() {
            var realm = Realm.GetInstance();
            return realm.WriteAsync(db => {
                var user = db.All<UserLogin>().First();
                user.Token = string.Empty;
            });
        }
    }
}

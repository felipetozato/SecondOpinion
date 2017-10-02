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
        readonly Realm realm;

        public SettingsRepository(Realm realm) {
            this.realm = realm;
        }

        /// <summary>
        /// Gets the user login.
        /// </summary>
        /// <returns>The user login.</returns>
        public UserLogin GetUserLogin()
        {
            return realm.All<UserLogin>().First();
        }

        /// <summary>
        /// Saves the user login.
        /// </summary>
        /// <param name="userLogin">User login.</param>
        public Task SaveOrUpdateUserLogin(UserLogin userLogin) {
            return realm.WriteAsync(realmTemp => {
                realmTemp.Add(userLogin, true);
            });
        }


    }
}

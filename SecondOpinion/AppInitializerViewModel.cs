using System;
using SecondOpinion.Repositories;
using Splat;
using Realms;
using SecondOpinion.Models;
using SecondOpinion.Services.Api;
using System.Threading.Tasks;
using ReactiveUI;
using System.Threading;
using System.Reactive.Linq;

namespace SecondOpinion
{
    public class AppInitializerViewModel
    {

        private static bool initialized;

        /// <summary>
        /// Initialize cross platform components.
        /// </summary>
        public static void Initialize() {
            Locator.CurrentMutable.RegisterConstant(new SettingsRepository(), typeof(ISettingsRepository));
            Locator.CurrentMutable.RegisterConstant(new SharedPreferencesRepository(), typeof(ISharedPreferences));

            initialized = true;
        }

        public async Task<bool> ShouldGoToLogin() {
            if (!initialized) {
                throw new ArgumentException("You must call Initialize first");
            }

            var settingsRepo = Locator.Current.GetService<ISettingsRepository>();
            var currentUser = settingsRepo.GetUserLogin();
            if (currentUser != null) {
                // Have to revalidate the login
                if (currentUser.TS < DateTime.Now.Ticks) {
                    var login = await ApiCoordinator.Login(currentUser.Email, currentUser.Password);
                    await settingsRepo.SaveOrUpdateUserLogin(login);
                }
                return false;
            } else {
                return true;
            }
        }
    }
}

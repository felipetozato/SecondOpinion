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
using SecondOpinion.ViewModels;

namespace SecondOpinion.ViewModels
{
    public class AppInitializerViewModel : BaseViewModel
    {

        private static bool initialized;

        /// <summary>
        /// Initialize cross platform components.
        /// </summary>
        public static void Initialize() {
            Locator.CurrentMutable.RegisterLazySingleton(() => new SettingsRepository(), typeof(ISettingsRepository));
            Locator.CurrentMutable.RegisterLazySingleton(() => new UserContactRepository(), typeof(IUserContactRepository));
            Locator.CurrentMutable.RegisterLazySingleton(() => new ChatRepository(), typeof(IChatRepository));
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
                if (currentUser.UpdatedAt.AddHours(2).Ticks < DateTime.Now.Ticks) {
                    string email = currentUser.Email;
                    string password = currentUser.Password;
                    await settingsRepo.InvalidateUserLogin(currentUser);
                    var login = await ApiCoordinator.Login(email, password);
                    await settingsRepo.SaveOrUpdateUserLogin(login);
                }
                return false;
            } else {
                return true;
            }
        }
    }
}

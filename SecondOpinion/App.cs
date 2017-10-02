using System;
using SecondOpinion.Repositories;
using Splat;
using Realms;

namespace SecondOpinion
{
    public class App
    {
        public static bool UseMockDataStore = true;

        /// <summary>
        /// Initialize cross platform components.
        /// </summary>
        public static void Initialize()
        {
            var realm = Realm.GetInstance();

            Locator.CurrentMutable.RegisterLazySingleton(() =>
            {
                return new SettingsRepository(realm);
            }, typeof(ISettingsRepository));
        }
    }
}

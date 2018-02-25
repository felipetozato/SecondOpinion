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
        public static void Initialize() {
            Locator.CurrentMutable.RegisterConstant(new SettingsRepository(), typeof(ISettingsRepository));
            Locator.CurrentMutable.RegisterConstant(new SharedPreferencesRepository(), typeof(ISharedPreferences));
        }
    }
}

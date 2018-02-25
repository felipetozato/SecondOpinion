using System;
using ReactiveUI;
using SecondOpinion.Repositories;
using SecondOpinion.Services;
using Splat;

namespace SecondOpinion.ViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        /// <summary>
        /// The router.
        /// </summary>
        //protected readonly IRouter Router;

        protected readonly ISettingsRepository UserSettings;

        public BaseViewModel() {
            //Router = Locator.Current.GetService<IRouter>();
            UserSettings = Locator.Current.GetService<ISettingsRepository>();
        }
    }
}

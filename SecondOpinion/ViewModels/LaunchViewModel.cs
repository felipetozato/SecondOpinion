using System;
using ReactiveUI;
using System.Reactive.Linq;
using Splat;
using SecondOpinion.Repositories;

namespace SecondOpinion.ViewModels {
    public class LaunchViewModel : BaseViewModel {

        private bool _IsDoingWork;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:SecondOpinion.ViewModels.LaunchViewModel"/> is
        /// doing work.
        /// </summary>
        /// <value><c>true</c> if is doing work; otherwise, <c>false</c>.</value>
        public bool IsDoingWork {
            get => _IsDoingWork;
            set => this.RaiseAndSetIfChanged(ref _IsDoingWork , value);
        }

        //public IObservable<bool> NeedsLogIn() {
        //    var userSettings = UserSettings.GetUserLogin();

        //    if (userSettings == null) {
        //        return Observable.Return(false);
        //    } else {
        //        var sharedPreferences = Locator.Current.GetService<ISharedPreferences>();

        //    }

        //}
    }
}

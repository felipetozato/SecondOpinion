using System;
using System.Reactive;
using ReactiveUI;
using System.Threading.Tasks;
using SecondOpinion.Repositories;
using Splat;
using SecondOpinion.Services.Api;

namespace SecondOpinion.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private string _emailAddress;
        public string EmailAddress {
            get { return _emailAddress; }
            set { this.RaiseAndSetIfChanged(ref _emailAddress, value); }
        }

        private string _password;
        public string Password {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ReactiveCommand<Unit, bool> LoginCommand;

        private ISharedPreferences sharedPreferences;

        public LoginViewModel() {
            LoginCommand = ReactiveCommand.CreateFromTask(DoLogin);
            sharedPreferences = Locator.Current.GetService<ISharedPreferences>();
        }

        private async Task<bool> DoLogin() {
            System.Diagnostics.Debug.WriteLine(String.Format("Login in with user: {0}, pass: {1}", EmailAddress, Password));
            var email = EmailAddress.Trim();
            var password = Password.Trim();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password)) {
                var worked = await ApiCoordinator.Login(email, password);
                if (worked != null) {
                    System.Diagnostics.Debug.WriteLine("SUCCESS!!!");
                    await UserSettings.SaveOrUpdateUserLogin(worked);
                    sharedPreferences.SetString(PreferencesKeys.EMAIL, EmailAddress);
                    sharedPreferences.SetString(PreferencesKeys.PASSWORD, Password);
                    return true;
                } else {
                    System.Diagnostics.Debug.WriteLine("DID NOT WORKED!!!");
                    return false;
                }
            }
            return false;
        }
    }
}

using System;
using System.Reactive;
using ReactiveUI;
using System.Threading.Tasks;
using IntenseCare.Services.Api;
using SecondOpinion.Repositories;
using Splat;

namespace IntenseCare.ViewModels
{
    public class LoginViewModel : ReactiveObject
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

        public ReactiveCommand<Unit, Unit> LoginCommand;

        public LoginViewModel() {
            LoginCommand = ReactiveCommand.CreateFromTask(DoLogin);
        }

        private async Task DoLogin() {
            System.Diagnostics.Debug.WriteLine(String.Format("Login in with user: {0}, pass: {1}", EmailAddress, Password));
            var email = EmailAddress.Trim();
            var password = Password.Trim();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password)) {
                var worked = await ApiCoordinator.Login(email, password);
                if (worked != null) {
                    System.Diagnostics.Debug.WriteLine("SUCCESS!!!");
                    await Locator.Current.GetService<ISettingsRepository>().SaveOrUpdateUserLogin(worked);
                } else {
                    System.Diagnostics.Debug.WriteLine("DID NOT WORKED!!!");
                }
            }
        }
    }
}

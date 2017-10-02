using System;
using System.Threading.Tasks;
using SecondOpinion.Models;
using Refit;

namespace IntenseCare.Services.Api
{
    public static class ApiCoordinator
    {
        const string SERVER_URL = "http://192.168.0.105:8080";

        readonly static IILoginApi loginApi;

        static ApiCoordinator() {
            loginApi = RestService.For<IILoginApi>(SERVER_URL);
        }

        /// <summary>
        /// Login with the specified email and password.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        public static async Task<UserLogin> Login(string email, string password) {
            var loginResquest = new LoginRequest(email, password);
            var userLogin = await loginApi.Login(loginResquest);
            userLogin.Email = email;
            userLogin.Password = password;
            return userLogin;
        }
    }
}

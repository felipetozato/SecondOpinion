﻿using System;
using System.Threading.Tasks;
using SecondOpinion.Models;
using Refit;
using SecondOpinion.Repositories;
using Splat;
using System.Collections.Generic;
using System.Net.Http;

namespace SecondOpinion.Services.Api
{
    public static class ApiCoordinator
    {
        const string SERVER_URL = "http://192.168.0.106:8080";

        readonly static ILoginApi loginApi;
        readonly static IChatApi chatApi;
        readonly static IUserApi userApi;
        readonly static ISettingsRepository userSettings;
        readonly static ISharedPreferences sharedPreferences;

        static ApiCoordinator() {
            userSettings = Locator.Current.GetService<ISettingsRepository>();
            sharedPreferences = Locator.Current.GetService<ISharedPreferences>();

            Func<long, Task> saveTime = (long time) => {
                return Task.Run(() => sharedPreferences.SetLong(PreferencesKeys.LAST_REQUEST, time));
            };

            var httpClient = new HttpClient(new CustomHttpClientHandler(GetToken, saveTime)) {
                BaseAddress = new Uri(SERVER_URL)
            };

            loginApi = RestService.For<ILoginApi>(httpClient);
            chatApi = RestService.For<IChatApi>(httpClient);
            userApi = RestService.For<IUserApi>(httpClient);
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

        public static Task<Page<Dialog>> GetAllChats() {
            return chatApi.GetAllChats();
        }

        public static Task<Page<UserContact>> GetAllContacts() {
            return userApi.GetAllContacts();
        }

        public static Task<Message> SendPrivateMessage(Message message) {
            return chatApi.SendPrivateMessage(message);
        }

        public static async Task<Page<Message>> GetPrivateMessages(string dialogId) {
            var result = await chatApi.GetPrivateMessages(dialogId);
            return result;
        }

        private static string GetToken() {
            var userLogin = userSettings.GetUserLogin();
            if (userLogin != null) {
                return userLogin.Token;
            }
            return string.Empty;
        }

        private class CustomHttpClientHandler : HttpClientHandler {

            private readonly Func<string> getToken;

            private Func<long , Task> saveTimeOfLastSuccessfulRequest;

            public CustomHttpClientHandler (Func<string> getToken, Func<long, Task> saveTimeOfLastSuccessfulRequest) {
                this.getToken = getToken;
                this.saveTimeOfLastSuccessfulRequest = saveTimeOfLastSuccessfulRequest;
            }

            protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage request , System.Threading.CancellationToken cancellationToken) {
                var token = getToken();
                if (!string.IsNullOrEmpty(token)) {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);    
                }

                var result = await base.SendAsync(request , cancellationToken);
                if (result.IsSuccessStatusCode) {
                    saveTimeOfLastSuccessfulRequest(DateTime.Now.Ticks).ConfigureAwait(false);
                }
                return result;
            }
        }
    }
}

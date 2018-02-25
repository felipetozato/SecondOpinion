using System;
using ReactiveUI;
using System.Reactive;
using SecondOpinion.Models;
using System.Threading.Tasks;
using SecondOpinion.Repositories;
using Splat;
using SecondOpinion.Services.Api;

namespace SecondOpinion.ViewModels
{
    public class DialogListViewModel : BaseViewModel {

        private ReactiveList<Dialog> _chatList;
        public ReactiveList<Dialog> ChatList {
            get => _chatList;
            private set => this.RaiseAndSetIfChanged(ref _chatList , value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SecondOpinion.ViewModels.ChatListViewModel"/> class.
        /// </summary>
        public DialogListViewModel() : base() {
            _chatList = new ReactiveList<Dialog>(); // empty initial list
        }

        /// <summary>
        /// Populate this instance.
        /// </summary>
        /// <returns>The populate.</returns>
        public async Task Populate() {
            await GetConversationFromServer();
        }

        private async Task GetConversationFromServer() {
            try {
                var result = await Task.Run(() => {
                    return ApiCoordinator.GetAllChats();
                });
                ChatList.AddRange(result.Items);
                System.Diagnostics.Debug.WriteLine("WORKED");
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

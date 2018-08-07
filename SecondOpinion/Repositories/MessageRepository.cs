using System;
using SecondOpinion.Models;
using SecondOpinion.Services.Api;
using Realms;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReactiveUI;
using System.Linq;
using System.Reactive.Linq;

namespace SecondOpinion.Repositories {
    public class MessageRepository : IMessageRepository {
        public MessageRepository () {
        }

        public IObservable<IEnumerable<Message>> GetAllMessages(string dialogId) {
            return Observable.Create( async (IObserver<IEnumerable<Message>> arg) => {
                //local data
                var localData = GetAllFromLocal(dialogId);
                arg.OnNext(localData);
                //web
                var webData = await GetAllFromWeb(dialogId);
                arg.OnNext(webData.Items);
                arg.OnCompleted();
            });
        }

        private IReadOnlyList<Message> GetAllFromLocal(string dialogId) {
            var realm = Realm.GetInstance();
            var result = realm.All<Message>().Where(mess => mess.ChatDialogId == dialogId).AsRealmCollection();
            return result;
        }

        private async Task<Page<Message>> GetAllFromWeb (string dialogId) {
            try {
                var results = await ApiCoordinator.GetPrivateMessages(dialogId);
                var realm = Realm.GetInstance();
                realm.Write(() => {
                    foreach (var item in results.Items) {
                        var result = realm.Add<Message>(item, true);
                    }
                });
                System.Diagnostics.Debug.WriteLine("WORKED");
                return results;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return Page<Message>.Empty;
        }
    }
}

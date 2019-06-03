using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;
using SecondOpinion.Models;
using SecondOpinion.Services.Api;
using Realms;
namespace SecondOpinion.Repositories {
    public class ChatRepository : IChatRepository {
        public ChatRepository () {
        }

        public IObservable<IEnumerable<Dialog>> GetAllDialogs() {
            return Observable.Create( async (IObserver<IEnumerable<Dialog>> arg) => {
                //local data
                var localData = GetAllDialogsFromLocal();
                arg.OnNext(localData);
                //web
                var webData = await GetAllDialogsFromWeb();
                arg.OnNext(webData.Items);
                arg.OnCompleted();
            });
        }

        public Dialog GetDialog (UserContact contact) {
            var realm = Realm.GetInstance();
            var result = realm.All<Dialog>();
            if (result == null || result.Count() == 0) {
                return null;
            } else {
                return result.AsEnumerable().FirstOrDefault(dialog => dialog.OccupantsIds?.Contains(contact.Id) ?? false);
            }
        }

        private IReadOnlyList<Dialog> GetAllDialogsFromLocal() {
            var realm = Realm.GetInstance();
            var result = realm.All<Dialog>().AsRealmCollection();
            return result;
        }

        private async Task<Page<Dialog>> GetAllDialogsFromWeb() {
            try {
                var results = await ApiCoordinator.GetAllChats();
                var realm = Realm.GetInstance();
                realm.Write(() => {
                    foreach (var item in results.Items) {
                        var result = realm.Add<Dialog>(item, true);
                    }
                });
                System.Diagnostics.Debug.WriteLine("WORKED");
                return results;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return Page<Dialog>.Empty;
        }
    }
}

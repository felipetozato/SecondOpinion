using System;
using System.Collections.Generic;
using SecondOpinion.Models;
using System.Reactive.Linq;
using Realms;
using System.Threading.Tasks;
using SecondOpinion.Services.Api;

namespace SecondOpinion.Repositories {
	public class UserContactRepository : IUserContactRepository {
        public UserContactRepository () {
			
        }

		public IObservable<IEnumerable<UserContact>> GetAllUserContact () {
			//Get from local first
            return Observable.Create(async (IObserver<IEnumerable<UserContact>> arg) => {
                //load local first
                //var localData = GetAllFromLocal();
                //arg.OnNext(localData);
                //load Internet stuff
                var webData = await GetAllFromWeb();
                arg.OnNext(webData.Items);
                arg.OnCompleted();
            });
		}

		private IReadOnlyList<UserContact> GetAllFromLocal() {
			var realm = Realm.GetInstance();
			var result = realm.All<UserContact>().AsRealmCollection();
            return result;
		}

        private async Task<Page<UserContact>> GetAllFromWeb () {
			try {
                var results = await ApiCoordinator.GetAllContacts();
                var realm = Realm.GetInstance();
                realm.Write(() => {
                    foreach (var item in results.Items) {
                        var result = realm.Add<UserContact>(item, true);
                    }
                });
                System.Diagnostics.Debug.WriteLine("WORKED");
                return results;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return Page<UserContact>.Empty;
		}
    }
}

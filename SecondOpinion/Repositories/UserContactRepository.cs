using System;
using System.Collections.Generic;
using SecondOpinion.Models;
using System.Reactive.Linq;
using Realms;
using System.Threading.Tasks;


namespace SecondOpinion.Repositories {
	public class UserContactRepository : IUserContactRepository {
        public UserContactRepository () {
			
        }

		public IObservable<IReadOnlyList<UserContact>> GetAllUserContact () {
			//Get from local first
			return GetAllFromLocal();
		}

		private IObservable<IReadOnlyList<UserContact>> GetAllFromLocal() {
			return Observable.Create<IReadOnlyList<UserContact>>(observer => () => {
				var realm = Realm.GetInstance();
				var result = realm.All<UserContact>().AsRealmCollection();
				observer.OnNext(result);
				observer.OnCompleted();
			});
		}

		private IObservable<IReadOnlyList<UserContact>> GetAllFromWeb () {
			return Observable.Create<IReadOnlyList<UserContact>>(observer => new Task(async () => {
				I
				try {
                    return ApiCoordinator.GetAllContacts();
                    ContactList.AddRange(result.Items.Select(UserListItem.Create));
                    System.Diagnostics.Debug.WriteLine("WORKED");
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
				var realm = Realm.GetInstance();
				var result = realm.All<UserCoontact>().AsRealmCollection();
				observer.OnNext(result);
				observer.OnCompleted();
			});
		}
    }
}

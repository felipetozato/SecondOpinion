using System;
using Realms;

namespace SecondOpinion.Repositories {
    public class SharedPreferencesRepository : ISharedPreferences {
        
        public int GetInt (string key) {
            var realm = Realm.GetInstance();
            var theValue = realm.Find<KeyValue>(key);
            return int.Parse(theValue.Value);
        }

        public long GetLong (string key) {
            var realm = Realm.GetInstance();
            var theValue = realm.Find<KeyValue>(key);
            return long.Parse(theValue.Value);
        }

        public string GetString (string key) {
            var realm = Realm.GetInstance();
            var theValue = realm.Find<KeyValue>(key);
            return theValue.Value;
        }

        public void SetInt (string key , int theValue) {
            var realm = Realm.GetInstance();
            var newItem = new KeyValue(key , theValue.ToString());
            realm.Write(() => realm.Add(newItem , true));
        }

        public void SetLong (string key , long theValue) {
            var realm = Realm.GetInstance();
            var newItem = new KeyValue(key , theValue.ToString());
            realm.Write(() => realm.Add(newItem , true));
        }

        public void SetString (string key , string theValue) {
            var realm = Realm.GetInstance();
            var newItem = new KeyValue(key , theValue);
            realm.Write(() => realm.Add(newItem , true));
        }

    }

    class KeyValue : RealmObject {

        [PrimaryKey]
        public string Key {
            get;
            set;
        }

        public string Value {
            get;
            set;
        }

        public KeyValue() {
            
        }

        public KeyValue (string key , string theValue) {
            Key = key;
            Value = theValue;
        }
    }
}

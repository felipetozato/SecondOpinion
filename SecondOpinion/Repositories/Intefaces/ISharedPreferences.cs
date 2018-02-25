using System;
namespace SecondOpinion.Repositories {
    public interface ISharedPreferences {

        void SetString (string key , string theValue);

        void SetInt (string key , int theValue);

        void SetLong (string key , long theValue);

        string GetString (string key);

        int GetInt (string key);

        long GetLong (string key);
    }
}

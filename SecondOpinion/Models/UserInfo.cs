using System;
using Newtonsoft.Json;
using Realms;

namespace SecondOpinion.Models {
    public class UserInfo : RealmObject {

        [JsonProperty("full_name")]
        public string Name {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Realms;

namespace SecondOpinion.Models
{
	public class UserContact : RealmObject
    {
		[PrimaryKey]
        [JsonProperty("id")]
        public long Id {
            get;
            set;
        }
        
        [JsonProperty("full_name")]
        public string Name {
            get;
            set;
        }

        [JsonProperty("email")]
        public string Email {
            get;
            set;
        }

        [JsonProperty("login")]
        public string UserName {
            get;
            set;
        }

        [JsonProperty("phone")]
        public string Phone {
            get;
            set;
        }

        [JsonProperty("user_tags")]
        public IList<string> Tags {
            get;
        }

        public UserContact() {
            
        }

        public static UserContact Create(long id, string name) {
            return new UserContact {
                Id = id,
                Name = name
            };
        }

        public override bool Equals (object obj) {
            return Id.Equals(obj);
        }
    }
}

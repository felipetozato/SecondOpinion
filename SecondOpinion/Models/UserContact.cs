using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecondOpinion.Models
{
    public class UserContact
    {
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
        public List<string> Tags {
            get;
            set;
        }

        public UserContact(long id, string name) {
            Id = id;
            Name = name;
        }
    }
}

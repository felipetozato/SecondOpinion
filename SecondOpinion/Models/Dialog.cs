using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Realms;

namespace SecondOpinion.Models {

    public class Dialog : RealmObject {

        [JsonProperty("_id")]
        public string Id {
            get;
            set;
        }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt {
            get;
            set;
        }

        [JsonProperty("last_message")]
        public string LastMessage {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }

        [JsonProperty("photo")]
        public string Photo {
            get;
            set;
        }

        [JsonProperty("type")]
        public int Type {
            get;
            set;
        }

        [JsonProperty("user_id")]
        public long UserId {
            get;
            set;
        }

        [JsonProperty("unread_messages_count")]
        public long UnreadCount {
            get;
            set;
        }

        [JsonProperty("occupants_ids")]
        private IList<long> _occupantsIds;

        public IList<long> OccupantsIds {
            get => _occupantsIds;
        }

        private UserContact _userContact;
        public UserContact User {
            get => _userContact;
            set {
                _userContact = value;
                UserId = _userContact.Id;
            }
        }

        public enum ChatType {
            PublicGroup = 1,
            PrivateGroup = 2,
            PrivateChat = 3
        }
    }
}

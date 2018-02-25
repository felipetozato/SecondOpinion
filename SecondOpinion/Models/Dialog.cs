using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecondOpinion.Models {

    public class Dialog {

        [JsonProperty("_id")]
        public string Id {
            get;
            set;
        }

        [JsonProperty("created_at")]
        public DateTime CreatedAt {
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
        public ChatType Type {
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
        public List<long> OccupantsIds {
            get;
            set;
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

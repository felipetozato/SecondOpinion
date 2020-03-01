using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Realms;
using System.Linq;

namespace SecondOpinion.Models {

    public class Dialog : RealmObject {

        [PrimaryKey]
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

        public string PhotoToShow {
            get {
                if (Photo != null) return Photo;
                return Type.Equals(ChatType.PrivateChat) ? "userSolid" : "groupSolid";
            }
        }

        public enum ChatType {
            PublicGroup = 1,
            PrivateGroup = 2,
            PrivateChat = 3
        }

        public Dialog() {

        }

        public Dialog(long[] users, long currentUserId, ChatType type, string name) {
            _occupantsIds = users;
            UserId = currentUserId;
            Type = (int) type;
            Name = name;
        }

        public override bool Equals (object obj) {
            if (obj is Dialog dialog) {
                return this.Id.Equals(dialog.Id);
            } else {
                return false;
            }

        }

        public override int GetHashCode () {
            const int prime = 31;
            return prime * this.Id.GetHashCode();
        }
    }
}

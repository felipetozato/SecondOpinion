using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using Realms;

namespace SecondOpinion.Models
{
    public class Message : RealmObject, IEquatable<Message> {

        [PrimaryKey]
        [JsonProperty("_id")]
        public string Id {
            get;
            set;
        }

        [JsonProperty("attachments")]
        private IList<string> _attachements;
        public IList<string> Attachements {
            get => _attachements;
        }

        public Dialog Dialog {
            get;
            set;
        }

        [JsonProperty("date_sent")]
        public long DateSent {
            get;
            set;
        }

        [JsonProperty("delivered_ids")]
        private IList<long> _deliveredIds;
        public IList<long> DeliveredIds {
            get => _deliveredIds;
        } 

        [JsonProperty("recipient_id")]
        public Nullable<long> ToUserId {
            get;
            set;
        }

        [JsonProperty("sender_id")]
        public string SenderId {
            get;
            set;
        }

        [JsonProperty("message")]
        public string MessageBody {
            get;
            set;
        }

        [JsonProperty("read_ids")]
        private IList<long> _readIds;
        public IList<long> ReadIds {
            get => _readIds;
        }

        [JsonProperty("read")]
        public bool Read {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }

        [Indexed]
        [JsonProperty("chat_dialog_id")]
        public String ChatDialogId {
            get;
            set;
        }

        public static Message Create(IList<string> attachaments, IList<long> deliveredIds, IList<long> readIds) {
            return new Message() {
                _attachements = attachaments,
                _deliveredIds = deliveredIds,
                _readIds = readIds
            };
        }

        public Message() {
            
        }

        public override bool Equals (object obj) {
            if (obj.GetType() == typeof(Message)) {
                return ((Message) obj).Id == this.Id;
            } else {
                return false;
            }
        }

        public override int GetHashCode () {
            return Id.GetHashCode();
        }

        public bool Equals (Message other) {
            return other.Id == this.Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using Realms;

namespace SecondOpinion.Models
{
    public class Message : RealmObject {

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
        public long ToUserId {
            get;
            set;
        }

        [JsonProperty("sender_id")]
        public long SenderId {
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
    }
}

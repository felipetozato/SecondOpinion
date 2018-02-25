using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecondOpinion.Models
{
    public class Message {

        [JsonProperty("_id")]
        public string Id {
            get;
            set;
        }

        [JsonProperty("attachments")]
        public List<string> Attachements {
            get;
            set;
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
        public List<long> DeliveredIds {
            get;
            set;
        }

        [JsonProperty("recipient_id")]
        public long ToUserId {
            get;
            set;
        }

        [JsonProperty("message")]
        public string MessageBody {
            get;
            set;
        }

        [JsonProperty("read_ids")]
        public List<long> ReadIds {
            get;
            set;
        }

        [JsonProperty("read")]
        public bool Read {
            get;
            set;
        }

        public Message() {
            
        }

        public Message (long toUserId, string message) {
            ToUserId = toUserId;
            MessageBody = message;
        }
    }
}

using System;
using Newtonsoft.Json;
using Realms;

namespace SecondOpinion.Models
{
    public class UserLogin : RealmObject
    {
        [JsonProperty("application_id")]
        public string ApplicationId {
            get;
            set;
        }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt {
            get;
            set;
        }

        [JsonProperty("id")]
        public long Id {
            get;
            set;
        }

        [JsonProperty("token")]
        public string Token {
            get;
            set;
        }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt {
            get;
            set;
        }

        [JsonProperty("ts")]
        public long TS {
            get;
            set;
        }

        [PrimaryKey]
        [JsonProperty("user_id")]
        public long UserId {
            get;
            set;
        }

        public string Email {
            get;
            set;
        }

        public string Password {
            get;
            set;
        }

        public override string ToString() {
            return string.Format("[UserLogin: ApplicationId={0}, CreatedAt={1}, Id={2}, Token={3}, UpdatedAt={4}, UserId={5}]", ApplicationId, CreatedAt, Id, Token, UpdatedAt, UserId);
        }
    }
}

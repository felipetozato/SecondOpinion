using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecondOpinion.Models {
    public class CreateGroupRequest {

        [JsonProperty("type")]
        private string Type;

        [JsonProperty("name")]
        public string GroupName {
            get;
            private set;
        }

        [JsonProperty("occupants_ids")]
        public IList<long> UsersIds {
            get;
            private set;
        }

        public CreateGroupRequest (string group, IList<long> userIds) {
            this.GroupName = group;
            this.UsersIds = userIds;
            this.Type = "group_private";
        }
    }
}

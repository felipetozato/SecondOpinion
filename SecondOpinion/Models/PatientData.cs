using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace SecondOpinion.Models {
    public class PatientData {

        [JsonProperty("Value")]
        public string Data {
            get;
            set;
        }

        [JsonProperty("Datetime")]
        public string HappenedAt {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecondOpinion.Models {
    
    public class Page<T> {

        public static Page<T> Empty = new Page<T> {
            TotalEntries = 0,
            Skip = 0,
            Limit = 0,
            Items = new List<T>()
        };

        [JsonProperty("total_entries")]
        public long TotalEntries {
            get;
            set;
        }

        [JsonProperty("skip")]
        public int Skip {
            get;
            set;
        }

        [JsonProperty("limit")]
        public int Limit {
            get;
            set;
        }

        [JsonProperty("items")]
        public IList<T> Items {
            get;
            set;
        }
    }
}

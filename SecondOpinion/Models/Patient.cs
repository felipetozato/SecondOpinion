using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace SecondOpinion.Models {

    public class Patient {

        public const string NAME_KEY = "Patient"; 

        public string Name {
            get;
            private set;
        }

        public JObject RawInfo {
            get;
            private set;
        }

        public Patient(string name, JObject rawInfo) {
            Name = name;
            RawInfo = rawInfo;
        }
    }
}

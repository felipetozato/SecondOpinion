using System;
using System.Threading.Tasks;
using Refit;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SecondOpinion.Services.Api {
    public interface IPatientApi {

        [Get("/patients")]
        Task<List<JObject>> GetAllPatients ();
    }
}

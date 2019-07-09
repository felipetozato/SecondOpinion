using System;
using System.Threading.Tasks;
using Refit;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SecondOpinion.Models;

namespace SecondOpinion.Services.Api {
    public interface IPatientApi {

        [Get("/patients")]
        Task<List<Patient>> GetAllPatients ();

        [Get("/patients/data")]
        Task<Dictionary<string, List<PatientData>>> GetPatientData([Query("name")] string name);
    }
}

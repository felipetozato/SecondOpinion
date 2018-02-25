using System;
using System.Threading.Tasks;
using Refit;
using SecondOpinion.Models;

namespace SecondOpinion.Services.Api {
    public interface IUserApi {

        [Get("/users")]
        Task<Page<UserContact>> GetAllContacts ();
    }
}

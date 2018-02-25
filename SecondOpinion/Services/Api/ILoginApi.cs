using System;
using System.Threading.Tasks;
using SecondOpinion.Models;
using Refit;

namespace SecondOpinion.Services.Api
{
    public interface ILoginApi
    {
        [Post("/login")]
        Task<UserLogin> Login([Body] LoginRequest loginRequest);
    }
}

using System;
using System.Threading.Tasks;
using SecondOpinion.Models;
using Refit;

namespace IntenseCare.Services.Api
{
    public interface IILoginApi
    {
        [Post("/login")]
        Task<UserLogin> Login([Body] LoginRequest loginRequest);
    }
}

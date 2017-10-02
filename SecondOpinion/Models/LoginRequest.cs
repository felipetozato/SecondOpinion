using Newtonsoft.Json;

namespace SecondOpinion.Models
{
    /// <summary>
    /// Login request model
    /// </summary>
    public class LoginRequest
    {
        [JsonProperty("email")]
        public string Email {
            get;
            private set;
        }

        [JsonProperty("password")]
        public string Password {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:IntenseCare.PCL.Services.Api.LoginRequest"/> class.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        public LoginRequest(string email, string password) {
            Email = email;
            Password = password;
        }
    }
}
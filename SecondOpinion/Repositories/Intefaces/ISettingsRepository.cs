using System;
using System.Threading.Tasks;
using SecondOpinion.Models;

namespace SecondOpinion.Repositories
{
    public interface ISettingsRepository
    {
        Task SaveOrUpdateUserLogin(UserLogin userLogin);

        UserLogin GetUserLogin();
    }
}

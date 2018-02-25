using System;
using SecondOpinion.Models;

namespace SecondOpinion.Services
{
    public interface IRouter
    {

        void OpenContactList();

        void OpenPrivateChat(UserContact user);

    }
}

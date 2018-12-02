using System;
using System.Collections.Generic;
using SecondOpinion.Models;
namespace SecondOpinion.Repositories {
    
    public interface IChatRepository {
        IObservable<IEnumerable<Dialog>> GetAllDialogs();

        Dialog GetDialog (UserContact contact);
    }
}

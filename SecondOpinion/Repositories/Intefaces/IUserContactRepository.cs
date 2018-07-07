using System;
using SecondOpinion.Models;
using System.Collections.Generic;

namespace SecondOpinion.Repositories {
    public interface IUserContactRepository {

		IObservable<IReadOnlyList<UserContact>> GetAllUserContact();
        
    }
}

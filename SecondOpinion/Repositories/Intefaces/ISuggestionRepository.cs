using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondOpinion.Repositories.Intefaces {
    public interface ISuggestionRepository {

        Task<IList<string>> GetSuggestionList (string userId);
    }
}

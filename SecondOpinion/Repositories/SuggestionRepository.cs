using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using SecondOpinion.Repositories.Intefaces;
using SecondOpinion.Services.Api;

namespace SecondOpinion.Repositories {
    public class SuggestionRepository : ISuggestionRepository {

        public SuggestionRepository () {
        }

        public Task<IList<string>> GetSuggestionList (string userId) {
            return ApiCoordinator.GetSuggestionList(userId);
        }
    }
}

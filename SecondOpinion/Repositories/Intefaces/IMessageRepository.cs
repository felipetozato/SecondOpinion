using System;
using System.Collections;
using SecondOpinion.Models;
using System.Collections.Generic;

namespace SecondOpinion.Repositories {
    public interface IMessageRepository {
        IObservable<IEnumerable<Message>> GetAllMessages(string dialogId);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using SecondOpinion.Models;

namespace SecondOpinion.Services.Api {
    public interface IChatApi {

        [Get("/chat")]
        Task<Page<Dialog>> GetAllChats ();

        [Post("/chat/private_message")]
        Task<Message> SendPrivateMessage ([Body] Message message);

        [Get("/chat/{dialogId}/history")]
        Task<Page<Message>> GetPrivateMessages ([AliasAs("dialogId")] string dialogId);

        [Post("/chat/group_dialog")]
        Task<Dialog> CreateGroupDialog ([Body] CreateGroupRequest createGroup);

        [Get("/chat/suggestions?doctor_id={doctorId}")]
        Task<IList<string>> GetSuggestionList (string doctorId);
    }
}

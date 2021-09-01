using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IIndividualChatRepository
    {
        Task Create(IndividualChat individualChat,CancellationToken cancellationToken);
        Task<List<string>> GetChatAsync(Guid currentUserId, Guid userId);
    }
}

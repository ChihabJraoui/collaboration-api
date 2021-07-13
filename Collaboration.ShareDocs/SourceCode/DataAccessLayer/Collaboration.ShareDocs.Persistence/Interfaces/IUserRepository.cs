using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUser(Guid userId, CancellationToken cancellationToken);

            
    }
}

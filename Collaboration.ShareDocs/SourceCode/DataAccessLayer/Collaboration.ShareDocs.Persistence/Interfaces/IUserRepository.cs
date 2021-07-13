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
        Task<ICollection<ApplicationUser>> GetUserByKeyword(string keyword, CancellationToken cancellationToken);
        Task<ICollection<ApplicationUser>> GetUsers(CancellationToken cancellationToken);

    }
}

using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IGroupRepository : IRepositoryBase<Group>
    {
        /// <summary>
        /// Get Followers
        /// </summary>
        /// <param name="group"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Group</returns>
        Task<Group> CreateGroup(Group group, CancellationToken cancellationToken);
    }
}

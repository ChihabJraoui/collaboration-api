using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IFollowRepository 
    {
        /// <summary>
        /// Get Followers
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ICollection<ApplicationUser>> GetFollowers(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Get Followings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ICollection<ApplicationUser>> GetFollowings(Guid userId, CancellationToken cancellationToken);
        Task<string> GetCurrentUser();
        Task<bool> IsFollowing(Guid id,string currentUserId);
    }
}

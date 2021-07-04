using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IFollowRepository : IRepositoryBase<Follow>
    {
        /// <summary>
        /// Get Followers
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetFollowers(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Get Followings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetFollowings(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        ///  GetFollower
        /// </summary>
        /// <param name=""></param>
        /// <returns>Guid</returns>
        Task<Follow> GetAsync(Guid followerId, Guid memberId);

        Task<string> GetCurrentUser();

        Task<Follow> GetFollowerById(Guid id);

        Task<Follow> IsFollowing(Guid id,string currentUserId);
    }
}

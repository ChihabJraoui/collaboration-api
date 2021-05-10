using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IFollowRepository : IRepositoryBase<Follow>
    {
        /// <summary>
        ///  FollowUser
        /// </summary>
        /// <param name="FollowUserCommand"></param>
        /// <returns>void</returns>
        Task AddAsync(Follow follow);

        /// <summary>
        ///  GetFollower
        /// </summary>
        /// <param name=""></param>
        /// <returns>Guid</returns>
        Task<Follow> GetAsync(Guid followerId, Guid memberId);
        Task<string> GetCurrentUser();
        Task<Follow> GetFollowerById(Guid id);
        Task<Follow> Delete(Follow follower);
    }
}

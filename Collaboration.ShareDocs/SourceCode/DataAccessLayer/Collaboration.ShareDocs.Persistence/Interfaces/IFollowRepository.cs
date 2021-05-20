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

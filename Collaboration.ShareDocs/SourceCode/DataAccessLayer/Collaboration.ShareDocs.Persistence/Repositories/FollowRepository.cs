using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowRepository(AppDbContext context, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<string> GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ApplicationUser>> GetFollowers(Guid userId, CancellationToken cancellationToken)
        {
            var followers = await _userManager.Users.Where(u => u.Id == userId).Include(u => u.Followers).Select(u => u.Followers).SingleOrDefaultAsync(cancellationToken);
            return followers;   
        }

        public async Task<ICollection<ApplicationUser>> GetFollowings(Guid userId, CancellationToken cancellationToken)
        {
            var followings = await _userManager.Users.Where(u => u.Id == userId).Include(u => u.Followings).Select(u => u.Followings).SingleOrDefaultAsync(cancellationToken);
            return followings;

        }

        public Task<bool> IsFollowing(Guid id, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}

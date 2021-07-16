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



    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ApplicationUser> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            var user = await this._userManager.Users.Where(u => u.Id == userId).Include(u=>u.Followings).Include(u => u.Followers).SingleOrDefaultAsync( cancellationToken);

            return user;
        }

        public async Task<ICollection<ApplicationUser>> GetUserByKeyword(string keyword, CancellationToken cancellationToken)
        {
            var response = await _userManager.Users.Where(w => w.UserName.Contains(keyword)).ToListAsync(cancellationToken);
            return response;

        }
        public async Task<ICollection<ApplicationUser>> GetUsers(CancellationToken cancellationToken)
        {
            var response = await _userManager.Users.ToListAsync(cancellationToken);
            return response;

        }
    }
}

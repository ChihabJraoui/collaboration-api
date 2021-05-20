using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class FollowRepository : GenericRepository<Follow>,IFollowRepository
    {
        private readonly AppDbContext _context;

        public FollowRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Follow> CreateAsync(Follow obj, CancellationToken cancellationToken)
        {
            await base.InsertAsync(obj, cancellationToken);

            return obj;
        }

        public Task<Follow> Delete(Follow follower)
        {
            throw new NotImplementedException();
        }

        public Task<Follow> GetAsync(Guid followerId, Guid memberId)
        {
            throw new NotImplementedException();
        }

        public Task<Follow> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public Task<Follow> GetFollowerById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Follow> IsFollowing(Guid id,string currentUserId)
        {
            return await dbSet.Where(w => w.FollowerId == new Guid(currentUserId) && w.FollowingId == id).FirstOrDefaultAsync();
        }

        public Task<Follow> UpdateAsync(Follow obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        bool IRepositoryBase<Follow>.Delete(Follow obj)
        {
            throw new NotImplementedException();
        }
    }
}

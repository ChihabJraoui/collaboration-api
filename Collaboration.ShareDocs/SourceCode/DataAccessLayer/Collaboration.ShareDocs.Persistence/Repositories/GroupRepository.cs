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
    public class GroupRepository : GenericRepository<Group>,IGroupRepository
    {
        public GroupRepository(AppDbContext context):base(context)
        {

        }

        public Task<Group> CreateAsync(Group obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> CreateGroup(Group group, CancellationToken cancellationToken)
        {
            await base.InsertAsync(group, cancellationToken);
            return group;
        }

        public async Task<Group> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var group = await dbSet.Where(w => w.GroupID == id).Include(m=>m.Members)
               .SingleOrDefaultAsync(cancellationToken);
            return group;
        }

        public async Task<List<Group>> GetGroupsAsync(ApplicationUser member, CancellationToken cancellationToken)
        {
            var groups = await dbSet.Where(w => w.Members.Contains(member)).ToListAsync();
            return groups;
        }

        public async Task<List<List<IndividualChat>>> GetHistory(Guid groupID, CancellationToken cancellationToken)
        {
            var history = await dbSet.Where(h => h.GroupID == groupID).Select(m => m.Messages).ToListAsync(cancellationToken);
            return history;
        }

        public async Task<List<List<ApplicationUser>>>GetMemberAsync(Guid guid, CancellationToken cancellationToken)
        {
            var members = await dbSet.Where(w => w.GroupID == guid).Select(m => m.Members).ToListAsync();
            return members;
        }

        public Task<Group> UpdateAsync(Group obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

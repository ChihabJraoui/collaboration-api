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

    public class UserProjectRepository : GenericRepository<UserProject>,IUserProjectRepository
    {
        private readonly AppDbContext _context;

        public UserProjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddMemberToProject(ApplicationUser user, Project project, CancellationToken cancellationToken)
        {
            var userProject =
                new UserProject
                {
                    Project = project,
                    User = user,
                };
           
            

            await dbSet.AddAsync(userProject, cancellationToken);
        }

        public async Task<List<UserProject>> GetUsers( Project project, CancellationToken cancellationToken)
        {
            return await dbSet.Where(w => w.ProjectId==project.Id).Include(u=>u.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<UserProject> UserProject(ApplicationUser user, Project project, CancellationToken cancellationToken)
        {
           var response= await dbSet.Where(w => w.UserId == user.Id && w.ProjectId == project.Id)
              .SingleOrDefaultAsync(cancellationToken);
            return response;
        }


    }
}

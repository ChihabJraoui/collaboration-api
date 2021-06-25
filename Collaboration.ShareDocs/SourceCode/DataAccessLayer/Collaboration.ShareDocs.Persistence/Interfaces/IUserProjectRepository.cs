using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IUserProjectRepository
    {
        Task<List<UserProject>> GetUsers( Project project, CancellationToken cancellationToken);
        Task<UserProject> UserProject(ApplicationUser user, Project project, CancellationToken cancellationToken);
        Task AddMemberToProject(ApplicationUser user, Project project, CancellationToken cancellationToken);
    }
}

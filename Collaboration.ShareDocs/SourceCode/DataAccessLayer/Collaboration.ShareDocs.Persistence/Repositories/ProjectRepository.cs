using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ICurrentUser _currentUserService;
        private readonly AppDbContext _context;

        public ProjectRepository(ICurrentUser currentUserService,
                                 AppDbContext context
                                 )
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<Project> CreateAsync(Project project, CancellationToken cancellationToken)
        {
            var newProject = await _context.AddAsync(project);
            await _context.SaveChangesAsync(cancellationToken);
            return newProject.Entity;
        }



        public async Task<Project> GetAsync(Guid projectId, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Where(w => w.Id == projectId)
                .Include(w => w.Folders).Include(x => x.Workspace).OrderBy(n => n.Created).SingleOrDefaultAsync(cancellationToken);
            return project;
        }


        async Task<bool> IRepositoryBase<Project>.DeleteAsync(Project project, CancellationToken cancellationToken)
        {

            _context.Projects.Remove(project);


            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Project> UpdateAsync(Project project, CancellationToken cancellationToken)
        {
            var projectData = await GetAsync(project.Id, cancellationToken);
            projectData.Label = project.Label;
            projectData.Description = project.Description;
            projectData.Icon = project.Icon;

            await _context.SaveChangesAsync(cancellationToken);
            return project;
        }

        public Task<List<Project>> GetByKeyWordAsync(string keyWord)
        {
            throw new NotImplementedException();
        }
    }
}

using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class ProjectRepository:IProjectRepository
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

        public async Task<Project> CreateAsync(Project project)
        {
            var newProject = await _context.AddAsync(project);
            await _context.SaveChangesAsync();
            return newProject.Entity;
        }

        public async Task<string> DeleteAsync(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return project.DeletedBy;  
        }

        public async Task<Project> GetAsync(Guid projectId)
        {
            var project = await _context.Projects.Where(w => w.ProjectId == projectId)
                .Include(w => w.Folders).Include(x => x.Workspace).OrderBy(n => n.Created).SingleOrDefaultAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(Project project, ICurrentUser currentUserService)
        {
            var projectData = await GetAsync(project.ProjectId);
            projectData.Label = project.Label;
            projectData.Description = project.Description;
            projectData.Icon = project.Icon;
            projectData.LastModifiedBy = currentUserService.Username;

            await _context.SaveChangesAsync();
            return project;
        }
    }
}

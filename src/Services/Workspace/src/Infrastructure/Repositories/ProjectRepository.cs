using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Project.Commands;
using Application.Project.Commands.DeleteProject;
using Application.Project.Commands.UpdateProject;
using Application.Project.Queries.GetProjectById;
using Application.Project.Queries.GetProjectByKeyWord;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository

    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(ICurrentUserService currentUserService,
                                 ApplicationDbContext context,
                                 IMapper mapper)
        {
            _currentUserService = currentUserService;
            _context = context;
            _mapper = mapper;

        }


        public async Task<Unit> AddMembersToProject(Guid MemberID, Guid ProjectId)
        {
            //var project = await _context.projects.Where(e => e.ProjectId == ProjectId).SingleOrDefaultAsync();
            //var member = await _context.Members.Where(e => e.MemberID == MemberID).SingleOrDefaultAsync();
            ////var many = await _context.ProjectsMembers.AddAsync();
            //project.ProjectsMembers.Add(new ProjectsMembers
            //{
            //    MemberID = MemberID,
            //    ProjectId = ProjectId,
            //    Member = member,
            //    Project = project
            //});
            //await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<string> CreateAsync(CreateProjectCommand command,Workspace workspace, ICurrentUserService _currentUserService)
        {
            var project = new Project
            {
                Label = command.Label,
                Description = command.Description,
                CreatedBy = _currentUserService.UserId,
                Workspace = workspace,   
            };

            await _context.AddAsync(project);
            await _context.SaveChangesAsync();
            return project.Label;
        }

        public async Task<DeleteProjectDto> DeleteAsync(DeleteProjectCommand query, ICurrentUserService currentUserService)
        {
            var project =  await _context.projects
                .Where(e => e.ProjectId == query.ProjectId)
                .SingleOrDefaultAsync();
             _context.projects.Remove(project);
            await _context.SaveChangesAsync();
            //SRP!!
            var result = new DeleteProjectDto
            {
                DeletedAt = project.DeletedAt,
                DeletedBy = currentUserService.UserId,
                Label = project.Label,
                ProjectId = project.ProjectId

            };
            return result ;

        }

        public async Task<Project> GetAsync(GetProjectByIdQuery query)
        {
            var project = await _context.projects.Where(w => w.ProjectId == query.Id)
                .Include(w =>w.Folders).Include(x => x.Workspace).OrderBy(n => n.Created).SingleOrDefaultAsync();
            if(project == null)
            {
                throw new NotFoundException(nameof(Project), query.Id);
            }
            return project;
        }

        public async Task<ProjectsDTOLists> GetByKeyWordAsync(GetProjectByKeyWordQuery query)
        {
            var result =  await _context.projects.ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .Where(p => p.Label.Contains(query.KeyWord)).ToListAsync();
            var resultDto = new ProjectsDTOLists
            {
                Projects = result
            };
            return resultDto;
        }

        public async Task<Unit> UpdateAsync(UpdateProjectCommand query,Project project, ICurrentUserService currentUserService)
        {
           
            project.Label = query.Label;
            project.Description = query.Description;
            project.Icon = query.Icon;
            project.LastModifiedBy = currentUserService.UserId;

            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

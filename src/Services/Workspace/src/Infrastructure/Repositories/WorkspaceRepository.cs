using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Repositories;
using Application.Workspace.Commands;
using Application.Workspace.Commands.DeleteWorkspace;
using Application.Workspace.Commands.UpdateWorkspace;
using Application.Workspace.Queries.GetAllWorkspaces;
using Application.Workspace.Queries.GetLastModifiedWorkspace;
using Application.Workspace.Queries.GetLastWorkspace;
using Application.Workspace.Queries.GetWorkspace;
using Application.Workspace.Queries.GetWorkspaceByKeyWord;
using Application.Workspace.Queries.GetWorkspacesCount;
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
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkspaceRepository(ApplicationDbContext context,IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Guid> CreateAsync(CreateWorkspaceCommand workspace, ICurrentUserService currentUserService)
        {
            var NewWorkspace = new Workspace
            {
                Name = workspace.Name,
                Description = workspace.Description,
                Image = workspace.Image,
                BookMark = false,
                IsPrivate = true,
                //add member to owner !!! TODO
                Owner = currentUserService.UserId,
                CreatedBy = currentUserService.UserId,
            };
            await this._context.Workspaces.AddAsync(NewWorkspace);
            await this._context.SaveChangesAsync();
            return NewWorkspace.Id;

            
        }

        public async Task<DeleteWorkspaceReturnDto> DeleteAsync(DeleteWorkspaceCommad workspaceId, ICurrentUserService currentUserService, CancellationToken cancellationToken)
        {
            var entity = await _context.Workspaces
                .Where(e => e.Id == workspaceId.WorkspaceId)
                .SingleOrDefaultAsync(cancellationToken)
                ;

            if(entity == null)
            {
                throw new NotFoundException(nameof(Workspace), workspaceId);
            }
             _context.Workspaces.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            var result = new DeleteWorkspaceReturnDto
            {
                Name = entity.Name,
                Description = entity.Description,
                DeletedBy = currentUserService.UserId,
                Projects = entity.Projects,
                DeletedAt = entity.DeletedAt
            };
            return result;
        }

        public async Task<WorkspacesDTOLists> GetAllAsync(GetAllWorkspaceQuery request, CancellationToken cancellationToken)
        {
            
            return new WorkspacesDTOLists
            {
                WorkspacesLists = await _context.Workspaces.ProjectTo<WorkspaceDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(n => n.Created).ToListAsync(cancellationToken)
            };
        
        }

        public async Task<Workspace> GetAsync(GetWorkspaceByIdQuery queryId)
        {
            var query = _context.Workspaces.Where(w => w.Id == queryId.WorkspaceRequestId)
                .Include(w=> w.Projects).OrderBy(n=>n.Created);
            var Workspace = await query.FirstOrDefaultAsync();
            return Workspace;
        }

        public async Task<WorkspaceDtoLists> GetByKeyWord(GetWorkspaceByKeyWord keyWord)
        {
            var workspacesList = await _context.Workspaces.ProjectTo<WorkspacesDtoKW>(_mapper.ConfigurationProvider)
                                .Where(w => w.Name.Contains(keyWord.KeyWord)).ToListAsync();
            var result = new WorkspaceDtoLists { Workspaces = workspacesList };
            return result;
        }

        public async  Task<int> GetCount(GetWorkspacesCountQuery request)
        {
            return await _context.Workspaces.CountAsync();
        }

        public async Task<LastWorkspaceDto> GetLastAsync(GetLastWorkspaceQuery request, CancellationToken cancellationToken)
        {
            var lastWorkspace = await _context.Workspaces.ProjectTo<LastWorkspaceDto>(_mapper.ConfigurationProvider)
               .OrderByDescending(w => w.Created).ToListAsync();
            var response = lastWorkspace.FirstOrDefault();
            return response;
        }

        public async Task<WorkspaceLastModifiedDto> GetLastModifiedAsync(GetLastModifiedWorkspaceQuery request, CancellationToken cancellationToken)
        {
            var lastModifiedWorkspace = await _context.Workspaces.ProjectTo<WorkspaceLastModifiedDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(w => w.LastModified).ToListAsync();
            var response = lastModifiedWorkspace.FirstOrDefault();
            return response;
        }

        public async Task<Core.Entities.Workspace> UpdataAsync(UpdateWorkspaceCommand workspace, ICurrentUserService currentUser, CancellationToken cancellationToken)
        {
            var query = new GetWorkspaceByIdQuery { 
                WorkspaceRequestId=workspace.WorkspaceId
            };
            var workspaceData = await GetAsync(query);
            if (workspaceData != null)
            {
                workspaceData.Name = workspace.Name;
                workspaceData.Description = workspace.Description;
                workspaceData.Image = workspace.Image;
                workspaceData.BookMark = workspace.BookMark;
                workspaceData.IsPrivate = workspace.IsPrivate;
                workspaceData.CreatedBy = currentUser.UserId;

                await this._context.SaveChangesAsync(cancellationToken);

            }
            return workspaceData;

        }
    }
}

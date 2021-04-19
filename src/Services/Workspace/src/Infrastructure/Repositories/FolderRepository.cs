using Application.Common.Interfaces;
using Application.Folder.Commands;
using Application.Folder.Commands.DeleteFolder;
using Application.Folder.Commands.UpdateFolder;
using Application.Folder.Queries;
using Application.Folder.Queries.GetAllFolder;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FolderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Core.Entities.Folder> AddAsync(Core.Commun.Component component, AddFolderCommand command,ICurrentUserService currentUserService,Core.Entities.Project project)
        {


            var parentRoot = await GetAsync(command.FolderParentId);
            var folder = new Core.Entities.Folder(component.Name) { 
                Parent = parentRoot,
                CreatedBy = currentUserService.UserId,
                Project = project,
            };
            if (command.FolderParentId.Equals(Guid.Empty))
            {
                parentRoot = folder;
            }
            
            //component.Parent = parentRoot;
            parentRoot.components.Add(folder);

            await _context.Folders.AddAsync(folder);
            await _context.SaveChangesAsync();

            return folder;
        }

        public async Task<DeleteFolderDto> DeleteAsync(DeleteFolderCommand query, ICurrentUserService currentUserService)
        {
            var folder = await _context.Folders
                .Where(e => e.FolderId  == query.FolderId)
                .SingleOrDefaultAsync();
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
            return new DeleteFolderDto
            {
                ProjectId = folder.FolderId,
                DeletedAt = folder.DeletedAt,
                DeletedBy = currentUserService.UserId,
                Name=folder.Name

            };
        }

        public async Task<GetFoldersDtoLists> GetAllAsync(GetAllFoldersQuery request, CancellationToken cancellationToken)
        {
            return new GetFoldersDtoLists
            {
                Folders = await _context.Folders.ProjectTo<GetFoldersDto>(_mapper.ConfigurationProvider)
                .OrderBy(n => n.Created).ToListAsync(cancellationToken)
            };
        }

        public async Task<Folder> GetAsync(Guid FolderID)
        {
            var Folder = await _context.Folders.Where(w => w.FolderId == FolderID).Include(y=>y.Parent).SingleOrDefaultAsync();

            return Folder;
        }

        public async Task<Folder> GetQueryAsync(GetFolderQuery FolderID)
        {
            var Folder = await _context.Folders.Where(w => w.FolderId == FolderID.FolderId).Include(y => y.components).Include(y=>y.Files).SingleOrDefaultAsync();

            return Folder;
        }

       

        public async Task<string> RenameAsync(RenameFolderCommand request,Folder Folder,ICurrentUserService currentUserService, CancellationToken cancellationToken)
        {
            Folder.Name = request.Name;
            Folder.LastModifiedBy = currentUserService.UserId;
            await _context.SaveChangesAsync(cancellationToken);
            return Folder.Name;



        }
        public async Task<bool> UniqueName(string name)
        {
            return await _context.Folders
                .AllAsync(n => n.Name != name);
        }
    }
}

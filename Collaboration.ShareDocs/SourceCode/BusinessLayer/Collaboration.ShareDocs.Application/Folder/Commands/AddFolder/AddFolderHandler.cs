using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Project.Queries.GetProjectById;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folder.Commands
{
    public class AddFolderHandler : IRequestHandler<AddFolderCommand, Core.Entities.Folder>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectRepository _projectRepository;

        public AddFolderHandler(IFolderRepository folderRepository,ICurrentUserService currentUserService,IProjectRepository projectRepository)
        {
            _folderRepository = folderRepository;
            _currentUserService = currentUserService;
            _projectRepository = projectRepository;
        }
        public async Task<Core.Entities.Folder> Handle(AddFolderCommand request, CancellationToken cancellationToken)
        {
            var existe = await _folderRepository.UniqueName(request.Name);
            if (!existe)
            {
                throw new BusinessRuleException("Is already existe");
            };
            var folder = new Core.Entities.Folder(request.Name);
            
            //requered the idProject
            var queryProject = new GetProjectByIdQuery
            {
                Id = request.ProjectId
            };

            var project = await _projectRepository.GetAsync(queryProject);
            if(project == null)
            {
                throw new BusinessRuleException("Project doesn't exist");

            }
            return  await _folderRepository.AddAsync(folder,request,_currentUserService,project);
        }
    }
}

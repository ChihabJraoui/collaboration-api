using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Folders.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Folders
{
    public class CreateFolderCommand:IRequest<ApiResponseDetails>
    {
        public string Name { get; set; }
        public Guid FolderParentId { get; set; }
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<CreateFolderCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
            {
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);
                if(project == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.ProjectId);
                    return ApiCustomResponse.NotFound(message);
                }
                var folderParent = await _unitOfWork.FolderRepository.GetAsync(request.FolderParentId, cancellationToken);
                //todo folderparent root
                
                    var newFolder = new Folder(request.Name)
                    {
                        Name = request.Name,
                        Parent = folderParent,
                        Project = project

                    };
                var folder = await _unitOfWork.FolderRepository.CreateAsync(newFolder,cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<FolderDto>(folder);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

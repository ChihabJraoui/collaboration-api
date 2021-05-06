using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Folders.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
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
    public class UpdateFolderCommand:IRequest<ApiResponseDetails>
    {
        public Guid FolderId { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<UpdateFolderCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork,
                           IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
            {
                var folder = await _unitOfWork.FolderRepository.GetAsync(request.FolderId, cancellationToken);
                // R01  
                if (folder == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.Name);
                    return ApiCustomResponse.NotFound(message);
                }
                // R01 Workspace label is unique
                while (request != null && request.Name != folder.Name)
                {
                    if (!await _unitOfWork.MethodRepository.Unique<Folder>(request.Name, "Name", cancellationToken))
                    {
                        var message = string.Format(Resource.Error_NameExist, request.Name);
                        return ApiCustomResponse.ValidationError(new Error("Name", message));
                    }
                    folder.Name = request.Name;
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                   
                }

                var response = _mapper.Map<FolderDto>(folder);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}

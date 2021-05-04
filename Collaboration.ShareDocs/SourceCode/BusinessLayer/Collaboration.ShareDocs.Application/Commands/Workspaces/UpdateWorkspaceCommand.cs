using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Resources;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces
{
    public class UpdateWorkspaceCommand : IRequest<ApiResponseDetails>
    {
        public Guid WorkspaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool BookMark { get; set; }
        public bool IsPrivate { get; set; }

        public class Handler : IRequestHandler<UpdateWorkspaceCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork,
                           ICurrentUserService currentUserService, 
                           IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService; 
                _mapper = mapper;
            }

            public async Task<ApiResponseDetails> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
            {
                var workspace = await _unitOfWork.WorkspaceRepository.GetAsync(request.WorkspaceId, cancellationToken);
                // R01 Workspace 
                if (workspace == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.WorkspaceId);
                    return ApiCustomResponse.NotFound(message);
                }
                // R01 Workspace label is unique
                if (!await _unitOfWork.MethodRepository.UniqueName<Workspace>(request.Name, cancellationToken))
                {
                    var message = string.Format(Resource.Error_NameExist, request.Name);
                    return ApiCustomResponse.ValidationError(new Error("Name", message));
                }
               

                workspace.Name = request.Name;
                workspace.Description = request.Description; 
                workspace.Image = request.Image;
                workspace.BookMark = request.BookMark;
                workspace.IsPrivate = request.IsPrivate;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<WorkspaceDto>(workspace);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

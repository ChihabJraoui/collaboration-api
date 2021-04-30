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

                if (workspace == null)
                {
                    return ApiCustomResponse.NotFound( $" The specified WorkspaceId '{request.WorkspaceId}' Not Found.");
                }
                // R01 Workspace label is unique
                if (!await _unitOfWork.MethodRepository.UniqueName(request.Name, cancellationToken))
                {
                    return ApiCustomResponse.ValidationError(new Error("Name", $" The specified Name '{request.Name}' already exists."));
                }
               

                workspace.Name = workspace.Name;
                workspace.Description = workspace.Description; 
                workspace.Image = workspace.Image;
                workspace.BookMark = workspace.BookMark;
                workspace.IsPrivate = workspace.IsPrivate;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<WorkspaceDto>(workspace);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

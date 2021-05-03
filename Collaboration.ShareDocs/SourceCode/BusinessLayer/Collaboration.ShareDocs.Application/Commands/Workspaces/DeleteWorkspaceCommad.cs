using AutoMapper;
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
    public class DeleteWorkspaceCommand: IRequest<ApiResponseDetails>
    {
        public Guid WorkspaceId { get; set; }

        public class Handler : IRequestHandler<DeleteWorkspaceCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork,
                           ICurrentUserService currentUserService,
                           IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
            {
                var workspace = await _unitOfWork.WorkspaceRepository.GetAsync(request.WorkspaceId, cancellationToken);

                if (workspace == null)
                {
                    return ApiCustomResponse.NotFound($" The specified WorkspaceId '{request.WorkspaceId}' Not Found.");
                }
                _unitOfWork.WorkspaceRepository.Delete(workspace);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ApiCustomResponse.ReturnedObject(workspace.Id);
            }
        }
    }
}

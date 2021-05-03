using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;
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
    public class GetWorkspaceCommand:IRequest<ApiResponseDetails>
    {
        public Guid WorkspaceId { get; set; }

        public class Handler : IRequestHandler<GetWorkspaceCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork,ICurrentUserService currentUserService,IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(GetWorkspaceCommand request, CancellationToken cancellationToken)
            {
                if(request.WorkspaceId == null)
                {
                    return ApiCustomResponse.IncompleteRequest();
                }
                var workspace =await _unitOfWork.WorkspaceRepository.GetAsync(request.WorkspaceId, cancellationToken);
                if (workspace == null)
                {
                    return ApiCustomResponse.NotFound($" The specified WorkspaceId '{request.WorkspaceId}' Not Found.");
                }
                var response = _mapper.Map<WorkspaceDto>(workspace);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

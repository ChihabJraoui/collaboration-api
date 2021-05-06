using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces
{
    public class GetLastModifiedWorkspace : IRequest<ApiResponseDetails>
    {
        public class Handler : IRequestHandler<GetLastModifiedWorkspace, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }

            public async Task<ApiResponseDetails> Handle(GetLastModifiedWorkspace request, CancellationToken cancellationToken)
            {
                var workspace = await _unitOfWork.WorkspaceRepository.GetLastModifiedAsync(cancellationToken);
                if(workspace == null)
                {
                    var message = string.Format(Resource.Error_NotFound);
                    return ApiCustomResponse.NotFound(message);
                }
                var response = _mapper.Map<WorkspaceDto>(workspace);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class GetProjectsCommandByWorkspaceId:IRequest<ApiResponseDetails>
    {
        public Guid WorkspaceId { get; set; }

        public class Handler : IRequestHandler<GetProjectsCommandByWorkspaceId, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ApiResponseDetails> Handle(GetProjectsCommandByWorkspaceId request, CancellationToken cancellationToken)
            {
                var workspace = await _unitOfWork.WorkspaceRepository.GetAsync(request.WorkspaceId, cancellationToken);

                if(workspace == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request);
                    return ApiCustomResponse.NotFound(message);
                }

                var projects = await _unitOfWork.ProjectRepository.GetByWorkspaceIdAsync(request.WorkspaceId, cancellationToken);

                var response = _mapper.Map<List<ProjectDto>>(projects);

                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

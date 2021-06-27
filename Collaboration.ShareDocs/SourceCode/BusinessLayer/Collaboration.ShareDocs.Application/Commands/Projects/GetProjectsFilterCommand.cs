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
    public class GetProjectsFilterCommand : IRequest<ApiResponseDetails>
    {
        public Guid? WorkspaceId { get; set; }

        public class Handler : IRequestHandler<GetProjectsFilterCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ApiResponseDetails> Handle(GetProjectsFilterCommand request, CancellationToken cancellationToken)
            {

                var projects = await _unitOfWork.ProjectRepository.FilterAsync(request.WorkspaceId, cancellationToken);

                var response = _mapper.Map<List<ProjectDto>>(projects);

                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

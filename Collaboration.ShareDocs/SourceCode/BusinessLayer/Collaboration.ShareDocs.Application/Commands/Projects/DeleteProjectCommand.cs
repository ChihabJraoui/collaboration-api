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
    public class DeleteProjectCommand:IRequest<ApiResponseDetails>
    {
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<DeleteProjectCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                if (request.ProjectId == null)
                {
                    return ApiCustomResponse.IncompleteRequest();
                }
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);
                if (project == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.ProjectId);
                    return ApiCustomResponse.NotFound(message);
                }
                _unitOfWork.ProjectRepository.Delete(project);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<ProjectDto>(project);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}

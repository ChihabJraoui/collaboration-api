using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
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

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class CreateProjectCommand:IRequest<ApiResponseDetails>
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public Guid WorkspaceId { get; set; }

        public class Handler : IRequestHandler<CreateProjectCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork,
                                         IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                var workspace = await _unitOfWork.WorkspaceRepository.GetAsync(request.WorkspaceId, cancellationToken);
                
                if(workspace == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.Label);
                    return ApiCustomResponse.NotFound(message);
                }
                
                if (!await _unitOfWork.MethodRepository.Unique<Project>(request.Label,"Label", cancellationToken))
                {
                    var message = string.Format(Resource.Error_NameExist, request.Label);
                    return ApiCustomResponse.ValidationError(new Error("Label", message));
                }

                var newProject = new Project()
                {
                    Label = request.Label,
                    Description = request.Description,
                    Workspace = workspace
                };

                await _unitOfWork.ProjectRepository.CreateAsync(newProject, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<ProjectDto>(newProject);
                return ApiCustomResponse.ReturnedObject(response);


            }
        }
    }
}

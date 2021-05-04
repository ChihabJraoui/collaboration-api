using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class UpdateProjectCommand:IRequest<ApiResponseDetails>
    {
        public Guid ProjectId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public class Handler : IRequestHandler<UpdateProjectCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMethodesRepository _methodesRepository;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork,
                                         ICurrentUserService currentUserService,
                                         IMethodesRepository methodesRepository,
                                         IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                this._methodesRepository = methodesRepository;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);
                // R01 Workspace 
                if (project == null)
                {
                    return ApiCustomResponse.NotFound($" The specified ProojectId '{request.ProjectId}' Not Found.");
                }
                // R01 Workspace label is unique
                if (!await _unitOfWork.MethodRepository.UniqueName<Project>(request.Label, cancellationToken))
                {
                    return ApiCustomResponse.ValidationError(new Error("Label", $" The specified Label '{request.Label}' already exists."));
                }


                project.Label = request.Label;
                project.Description = request.Description;
                project.Icon = request.Icon;
                
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<ProjectDto>(project);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

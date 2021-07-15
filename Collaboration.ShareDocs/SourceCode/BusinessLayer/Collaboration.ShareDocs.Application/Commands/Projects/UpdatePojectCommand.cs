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
                // R01 Project 
                if (project == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.Label);
                    return ApiCustomResponse.NotFound(message);
                }
                // R01 Project label is unique
                while(request != null && request.Label != project.Label)
                {
                    if (!await _unitOfWork.MethodRepository.Unique<Project>(request.Label, "Label", cancellationToken))
                    {
                        var message = string.Format(Resource.Error_NameExist, request.Label);
                        return ApiCustomResponse.ValidationError(new Error("Label", message));
                    }
                    break;
                }
               

                if(request.Label != "")
                {
                    project.Label = request.Label;
                }
                if (request.Description  != "")
                {
                    project.Description = request.Description;
                }
                if (request.Icon != "")
                {
                    project.Icon = request.Icon;
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<ProjectDto>(project);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

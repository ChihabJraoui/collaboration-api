using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class GetUsersProjectCommand : IRequest<ApiResponseDetails>
    {
      
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<GetUsersProjectCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService,
                                         IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<ApiResponseDetails> Handle(GetUsersProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);
                if (project == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.ProjectId);
                    return ApiCustomResponse.NotFound(message);
                }

                var usersProject = await _unitOfWork.UserProjectRepository.GetUsers(project, cancellationToken);
               
                var response = _mapper.Map<List<UsersProjectDto>>(usersProject);
                
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}
using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
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
    public class AddUserToProject : IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<AddUserToProject, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;

            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserProjectRepository _userProjectRepository;

            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService,IUserProjectRepository userProjectRepository,
                                         IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userManager = userManager;
                _userProjectRepository = userProjectRepository;
            }

            public async Task<ApiResponseDetails> Handle(AddUserToProject request, CancellationToken cancellationToken)
            {
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);
                if (project == null)
                {
                    var message = string.Format(Resource.Error_NotFound, project,request.ProjectId);
                    return ApiCustomResponse.NotFound(message);
                }

                var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, user,request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                var excestingUser = await _unitOfWork.UserProjectRepository.UserProject(user, project, cancellationToken);
                if (excestingUser != null)
                {
                    var message = string.Format(Resource.Error_IsAleradyExist);
                    return ApiCustomResponse.NotFound(message);
                }

                await _unitOfWork.UserProjectRepository.AddMemberToProject(user, project, cancellationToken);

                await _unitOfWork.SaveChangesAsync();

                return ApiCustomResponse.ReturnedObject();

            }
        }
    }
}

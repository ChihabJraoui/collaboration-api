using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class GetUsersNotBelongsToProject : IRequest<ApiResponseDetails>
    {
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<GetUsersNotBelongsToProject, ApiResponseDetails>
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

            public async Task<ApiResponseDetails> Handle(GetUsersNotBelongsToProject request, CancellationToken cancellationToken)
            {
                var allUsers =  _userManager.Users.ToList();
                var users = await _unitOfWork.UserProjectRepository.GetUsers(request.ProjectId, cancellationToken);

                var usersResponse = allUsers.Except(users).ToList(); 
                var response = _mapper.Map<List<UserProfileDto>>(usersResponse);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
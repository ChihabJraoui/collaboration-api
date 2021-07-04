using AutoMapper;
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

namespace Collaboration.ShareDocs.Application.Commands.Follows
{
    public class IsFollowingCommand : IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<IsFollowingCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IUnitOfWork unitOfWork,
                UserManager<ApplicationUser> userManager,
                ICurrentUserService currentUserService,
                IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                this._userManager = userManager;
            }

            public async Task<ApiResponseDetails> Handle(IsFollowingCommand request, CancellationToken cancellationToken)
            {
                var IsFollowing = false;

                var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }

                if (_currentUserService.IsAuthenticated == false)
                {
                    var message = string.Format(Resource.Error_NotValid);
                    return ApiCustomResponse.NotFound(message);
                }

                var response = await _unitOfWork.FollowRepository.IsFollowing(user.Id, _currentUserService.UserId);
                
                if (response != null)
                {
                    IsFollowing = true;
                }

                return ApiCustomResponse.ReturnedObject(IsFollowing);

            }
        }
    }
}

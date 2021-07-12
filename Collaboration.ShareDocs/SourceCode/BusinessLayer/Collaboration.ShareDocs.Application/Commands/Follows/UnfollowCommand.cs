using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Follows.Dto;
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
    public class UnfollowCommand:IRequest<ApiResponseDetails>
    {
        public Guid FollowingId { get; set; }

        public class Handler : IRequestHandler<UnfollowCommand, ApiResponseDetails>
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

            public async Task<ApiResponseDetails> Handle(UnfollowCommand request, CancellationToken cancellationToken)
            {
                //var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == request.FollowingId, cancellationToken);

                //if (user == null)
                //{
                //    var message = string.Format(Resource.Error_NotFound, request.FollowingId);
                //    return ApiCustomResponse.NotFound(message);
                //}
                //var isFollowing = await this._unitOfWork.FollowRepository.IsFollowing(request.FollowingId, _currentUserService.UserId);
                //if(isFollowing == null)
                //{
                //    var message = string.Format(Resource.Error_NotFound, request.FollowingId);
                //    return ApiCustomResponse.NotFound(message);
                //}
                //var follower = _unitOfWork.FollowRepository.Delete(isFollowing);
                //await _unitOfWork.SaveChangesAsync(cancellationToken);
                //var response = _mapper.Map<FollowerDto>(isFollowing);
                //return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}

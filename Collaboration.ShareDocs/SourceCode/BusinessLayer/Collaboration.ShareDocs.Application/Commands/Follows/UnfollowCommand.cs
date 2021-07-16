using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Follows.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence;
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
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<UnfollowCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly IFollowRepository _followRepository;
            private readonly AppDbContext _appDbContext;

            public Handler(IUnitOfWork unitOfWork,
                ICurrentUserService currentUserService,
                IUserRepository userRepository,
                IFollowRepository followRepository,AppDbContext appDbContext,
                IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userRepository = userRepository;
                _followRepository = followRepository;
                _appDbContext = appDbContext;
                
            }

            public async Task<ApiResponseDetails> Handle(UnfollowCommand request, CancellationToken cancellationToken)
            {

                var user = await _userRepository.GetUser(request.UserId, cancellationToken);
                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                var isFollowing = await _followRepository.GetFollowings(new Guid(_currentUserService.UserId), cancellationToken);
                if (isFollowing.Contains(user) == false)
                {
                    var message = string.Format(Resource.Error_IsAleradyExist, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                var me = await _userRepository.GetUser(new Guid(_currentUserService.UserId), cancellationToken);
                var res=me.Followings.Remove(user);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return ApiCustomResponse.ReturnedObject();

            }
        }
    }
}

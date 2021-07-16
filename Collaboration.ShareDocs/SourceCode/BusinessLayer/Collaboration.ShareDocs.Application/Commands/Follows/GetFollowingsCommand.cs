using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Follows.Dto;
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Follows
{
    public class GetFollowingsCommand : IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<GetFollowingsCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly IFollowRepository _followRepository;

            public Handler(IUnitOfWork unitOfWork,
                IUserRepository userRepository,
                IFollowRepository followRepository,
                ICurrentUserService currentUserService,
                IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userRepository = userRepository;
                _followRepository = followRepository;
            }

            public async Task<ApiResponseDetails> Handle(GetFollowingsCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUser(request.UserId, cancellationToken);

                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                var followings = await _followRepository.GetFollowings(user.Id, cancellationToken);


                var response = _mapper.Map<List<ResponseUserDto>>(followings);
                return ApiCustomResponse.ReturnedObject(response);
                
            }
        }
    }
}

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
    public class GetFollowersCommand : IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<GetFollowersCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFollowRepository _followRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public Handler(IUnitOfWork unitOfWork,
                IUserRepository userRepository,
                IFollowRepository followRepository,
                ICurrentUserService currentUserService,
                IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _followRepository=followRepository;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                this._userRepository = userRepository;
            }
            
            public async Task<ApiResponseDetails> Handle(GetFollowersCommand request, CancellationToken cancellationToken)
            {

                var user = await _userRepository.GetUser(request.UserId, cancellationToken);

                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                var followers = await _followRepository.GetFollowers(user.Id, cancellationToken);
               

                var response = _mapper.Map<List<ResponseUserDto>>(followers);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}

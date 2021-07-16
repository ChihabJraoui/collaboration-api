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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Users
{
    public class GetUserById: IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public class Handler : IRequestHandler<GetUserById, ApiResponseDetails>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            private readonly ICurrentUserService _currentUserService;

            public Handler(
                IUserRepository userRepository,
                IMapper mapper,
                ICurrentUserService currentUserService)
            {
                this._userRepository = userRepository;
                this._mapper = mapper;
                this._currentUserService = currentUserService;
            }

            public async Task<ApiResponseDetails> Handle(GetUserById request, CancellationToken cancellationToken)
            {
                var user = await this._userRepository.GetUser(request.UserId,cancellationToken);


                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }

                var response = _mapper.Map<UserProfileDto>(user);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }

}

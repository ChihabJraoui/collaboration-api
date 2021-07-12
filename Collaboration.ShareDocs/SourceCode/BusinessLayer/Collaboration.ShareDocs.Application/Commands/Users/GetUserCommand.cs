using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.ShareDocs.Application.Commands.Users
{
    public class GetUserCommand : IRequest<ApiResponseDetails>
    {
        public string Username { get; set; }

        public class Handler : IRequestHandler<GetUserCommand, ApiResponseDetails>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            private readonly IMapper _mapper;

            private readonly ICurrentUserService _currentUserService;

            public Handler(
                UserManager<ApplicationUser> userManager,
                IMapper mapper,
                ICurrentUserService currentUserService )
            {
                this._userManager = userManager;
                this._mapper             = mapper;
                this._currentUserService = currentUserService;
            }

            public async Task<ApiResponseDetails> Handle( GetUserCommand request, CancellationToken cancellationToken )
            {
                var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.UserName == request.Username , cancellationToken);
                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.Username);
                    return ApiCustomResponse.NotFound(message);
                }
                var response = _mapper.Map<UserProfileDto>(user);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
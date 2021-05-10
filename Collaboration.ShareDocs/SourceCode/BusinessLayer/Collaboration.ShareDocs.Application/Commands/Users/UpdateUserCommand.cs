using AutoMapper;
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

namespace Collaboration.ShareDocs.Application.Commands.Users
{
    public class UpdateUserCommand:IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, ApiResponseDetails>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMapper _mapper;
            private readonly ICurrentUserService _currentUserService;

            public Handler(
                UserManager<ApplicationUser> userManager,
                IMapper mapper,
                ICurrentUserService currentUserService)
            {
                this._userManager = userManager;
                this._mapper = mapper;
                this._currentUserService = currentUserService;
            }
            public async Task<ApiResponseDetails> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);
                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                
                await _userManager.SetUserNameAsync(user, request.UserName);
                await _userManager.SetEmailAsync(user, request.Email);


                var response = _mapper.Map<UserDto>(user);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}

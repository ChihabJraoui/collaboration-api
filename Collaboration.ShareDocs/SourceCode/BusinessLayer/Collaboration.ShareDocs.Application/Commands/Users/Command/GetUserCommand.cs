using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Interfaces;
using Collaboration.ShareDocs.Persistence.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.ShareDocs.Application.Commands.Users.Command
{
    public class GetUserCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public class GetUserCommandHandler : IRequestHandler<GetUserCommand, string>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            private readonly IMapper _mapper;

            private readonly ICurrentUserService _currentUserService;

            public GetUserCommandHandler(
                UserManager<ApplicationUser> userManager,
                IMapper mapper,
                ICurrentUserService currentUserService )
            {
                this._userManager        = userManager;
                this._mapper             = mapper;
                this._currentUserService = currentUserService;
            }

            public async Task<string> Handle( GetUserCommand request, CancellationToken cancellationToken )
            {
                var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == request.Id, cancellationToken);


                if ( user == null )
                {
                    //return ApiCustomResponse.NotFound("User", request.Id.ToString());
                    return null;
                }


                return null;
            }
        }
    }
}
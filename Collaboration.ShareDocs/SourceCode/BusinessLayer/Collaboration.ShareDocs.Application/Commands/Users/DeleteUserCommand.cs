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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Users
{
    public class DeleteUserCommand:IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<DeleteUserCommand, ApiResponseDetails>
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
            public async Task<ApiResponseDetails> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);
                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                var response= await _userManager.DeleteAsync(user);
                return ApiCustomResponse.ReturnedObject(response);


            }
        }
    }
}

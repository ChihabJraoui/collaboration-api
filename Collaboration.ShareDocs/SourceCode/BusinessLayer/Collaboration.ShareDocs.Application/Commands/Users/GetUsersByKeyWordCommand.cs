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
    public class GetUsersByKeyWordCommand : IRequest<ApiResponseDetails>
    {
        public string Keyword { get; set; }

        public class Handler : IRequestHandler<GetUsersByKeyWordCommand, ApiResponseDetails>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService, IMapper mapper)
            {
                _userManager = userManager;
                _currentUserService = currentUserService;
                _mapper = mapper;

            }

            public async Task<ApiResponseDetails> Handle(GetUsersByKeyWordCommand request, CancellationToken cancellationToken)
            {
                if(request.Keyword == null)
                {
                    var message = string.Format(Resource.Error_NotValid, request);
                    return ApiCustomResponse.NotValid(request.Keyword, message, "");
                }
                var response = await _userManager.Users.Where(w => w.UserName.Contains(request.Keyword)).Select(x => x.UserName).ToListAsync(cancellationToken);
                if (response.Count == 0)
                {
                    var message = string.Format(Resource.Error_NotFound, request);
                    return ApiCustomResponse.NotFound(message);
                }
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}
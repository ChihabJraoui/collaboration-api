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
    public class GetUsersByKeyWordCommand : IRequest<ApiResponseDetails>
    {
        public string Keyword { get; set; }

        public class Handler : IRequestHandler<GetUsersByKeyWordCommand, ApiResponseDetails>
        {
            private readonly IUserRepository _userRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(IUserRepository userRepository, ICurrentUserService currentUserService, IMapper mapper)
            {
                _userRepository = userRepository;
                _currentUserService = currentUserService;
                _mapper = mapper;

            }

            public async Task<ApiResponseDetails> Handle(GetUsersByKeyWordCommand request, CancellationToken cancellationToken)
            {
                if(request.Keyword == null)
                {
                    var allUsers = await _userRepository.GetUsers(cancellationToken);
                    var resp = _mapper.Map<List<ResponseUserDto>>(allUsers);
                    return ApiCustomResponse.ReturnedObject(resp);
                }
                
                var users = await _userRepository.GetUserByKeyword(request.Keyword, cancellationToken);
                var response = _mapper.Map<List<ResponseUserDto>>(users);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}
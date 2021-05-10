using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Authentication.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Application.Common.Services;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Collaboration.ShareDocs.Application.Commands.Authentication
{
    public class LoginUserCommand : IRequest<ApiResponseDetails>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApiResponseDetails>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly RoleManager<ApplicationRole> _roleManager;
            private readonly IConfiguration _configuration;
            private readonly HttpContext _httpContext;
            private readonly IMapper _mapper;

            private readonly IJwtService _jwtService;

            public LoginUserCommandHandler(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                RoleManager<ApplicationRole> roleManager,
                IHttpContextAccessor httpContextAccessor,
                IMapper mapper,
                IJwtService jwtService)
            {
                this._userManager = userManager;
                this._signInManager = signInManager;

                this._configuration = configuration;
                this._roleManager   = roleManager;
                this._httpContext   = httpContextAccessor.HttpContext;
                this._mapper        = mapper;
                _jwtService         = jwtService;
            }

            public async Task<ApiResponseDetails> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = this._userManager.Users.SingleOrDefault(u => u.Email == request.Email);

                if (user == null)
                {
                    return new ApiResponseDetails
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        StatusName = nameof(HttpStatusCode.BadRequest),
                        Message = "Incorrect credentials."
                    };
                }

                //if (user.EmailConfirmed == false)
                //{
                //    var message = string.Format(Resource.Error_EmailNotConfirmed, request.Email);
                //    return ApiCustomResponse.ValidationError(new Error("Email", message));
                //}

                var signinResult = await this._signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
                if (!signinResult.Succeeded)
                {

                    return new ApiResponseDetails
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        StatusName = nameof(HttpStatusCode.BadRequest),
                        Message = "Incorrect credentials."
                    };
                }

                var responseUser = _mapper.Map<ResponseUserLoginDto>(user);



                responseUser.Token = _jwtService.GenerateJwtToken(user);

                responseUser.RefreshToken = this._jwtService.GenerateRandomToken( );


                if (this._jwtService.UsersRefreshTokens.ContainsKey(responseUser.Id.ToString()))
                {
                    this._jwtService.UsersRefreshTokens[responseUser.Id.ToString()] = responseUser.RefreshToken;
                }
                else
                {
                    this._jwtService.UsersRefreshTokens.Add(responseUser.Id.ToString(), responseUser.RefreshToken);
                }

                return ApiCustomResponse.ReturnedObject(responseUser);
            }
        }
    }
}

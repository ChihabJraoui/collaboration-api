using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Application.Common.Services;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.ShareDocs.Application.Commands.Authentication
{
  
    public class RegisterUserCommand : IRequest<ApiResponseDetails>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public class Handler : IRequestHandler<RegisterUserCommand, ApiResponseDetails>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IServiceHelper _serviceHelper;
            private readonly IMapper _mapper;

            private readonly ICurrentUserService _currentUserService;

            public Handler(
                UserManager<ApplicationUser> userManager,
                IMapper mapper,
                ICurrentUserService currentUserService,
                IServiceHelper serviceHelpers)
            {
                this._userManager        = userManager;
                this._mapper             = mapper;
                this._currentUserService = currentUserService;
                _serviceHelper           = serviceHelpers;
            }

            public async Task<ApiResponseDetails> Handle( RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var isEmail = this._serviceHelper.IsEmail( request.Email );
                if ( !isEmail )
                {
                    var message = string.Format(Resource.Error_NotValid_EmailForm, request.Email);
                    return ApiCustomResponse.ValidationError(new Error("Email", message));
                }
                if (await _userManager.Users.SingleOrDefaultAsync( u=>u.Email ==request.Email, cancellationToken ) !=null)
                {
                    var message = string.Format(Resource.Error_NameExist, request.Email);
                    return ApiCustomResponse.ValidationError(new Error("Email", message));
                }

                // check password validity
                var passwordValidationResult = await _serviceHelper.ValidatePassword( request.Password );
                if ( !passwordValidationResult.isValid )
                {
                    return ApiCustomResponse.ValidationErrors( passwordValidationResult.errors );
                }
                var user = new ApplicationUser
                {
                    Email          = request.Email,
                    UserName       = "@"+request.FirstName+request.LastName,
                    FirstName      = request.FirstName,
                    LastName       = request.LastName,
                    PhoneNumber    = request.PhoneNumber
                };

                var result = await this._userManager.CreateAsync( user, request.Password );

                var errors = new List<Error>( );

                if ( !result.Succeeded )
                {
                    errors.AddRange( result.Errors.Select( e => new Error( e.Code, e.Description ) ) );
                    return ApiCustomResponse.ValidationErrors( errors );
                }

                //Todo : confirm email using confirmedEmailCommand  then set isActive and emailConfirmed  to true


                return ApiCustomResponse.ReturnedObject( user );
            }
            
        }
    }
}

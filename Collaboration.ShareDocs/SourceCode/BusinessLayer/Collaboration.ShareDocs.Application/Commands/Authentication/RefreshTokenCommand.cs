using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Collaboration.ShareDocs.Application.Commands.Authentication
{
    public class RefreshTokenCommand : IRequest<ApiResponseDetails>
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public class Handler : IRequestHandler<RefreshTokenCommand, ApiResponseDetails>
        {
            private readonly IJwtService _jwtService;

            public Handler( IConfiguration configuration, IJwtService jwtService )
            {
                this.Configuration = configuration;
                _jwtService        = jwtService;
            }

            public IConfiguration Configuration { get; }

            public async Task<ApiResponseDetails> Handle( RefreshTokenCommand request,
                CancellationToken cancellationToken )
            {
                var           signinKey    = this.Configuration[ "Jwt:SigningKey" ];
                var           tokenHandler = new JwtSecurityTokenHandler( );
                SecurityToken validatToken;

                var principles = tokenHandler.ValidateToken(
                    request.Token,
                    new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey         = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( signinKey ) ),
                        ValidateIssuer           = false,
                        ValidateAudience         = false,
                        ValidateLifetime         = false
                    },
                    out validatToken
                );

                var jwtToken = validatToken as JwtSecurityToken;

                if ( jwtToken == null || !jwtToken.Header.Alg.Equals( SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                ) )
                {
                    var message = "Token not found";
                    return ApiCustomResponse.NotFound( message );
                }

                var userId = principles.Claims.SingleOrDefault( c => c.Type == "nameid" )?.Value.ToString( );
                var storedRefreshToken = this._jwtService.UsersRefreshTokens[ userId ];

                if ( request.RefreshToken != storedRefreshToken )
                {
                    var message = "Token not found";
                    return ApiCustomResponse.NotFound( message );
                }

                var credResponse = this._jwtService.Authenticate( userId, principles.Claims.ToArray( ) );

                return ApiCustomResponse.ReturnedObject( credResponse );
            }
        }
    }
}

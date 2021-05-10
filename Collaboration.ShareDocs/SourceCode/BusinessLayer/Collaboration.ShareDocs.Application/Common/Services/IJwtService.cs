using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Collaboration.ShareDocs.Persistence.Entities;

namespace Collaboration.ShareDocs.Application.Common.Services
{
    public interface IJwtService
    {
        string GenerateRandomToken();

        IDictionary<string, string> UsersRefreshTokens { get; set; }

        object Authenticate(string userId, Claim[] claims);
        string GenerateJwtToken(ApplicationUser user);

    }
}

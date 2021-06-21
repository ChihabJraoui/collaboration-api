using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    { 

        private ClaimsPrincipal _claimsPrincipal;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {

            _claimsPrincipal = httpContextAccessor?.HttpContext?.User;
            //.FindFirst("nameid").Value; 
        }




        private object GetPrincipipal()
        {
            if (_claimsPrincipal != null)
            {
                var firstClaims = _claimsPrincipal.FindFirst("nameid");
                if (firstClaims != null)
                {
                    var claimValue = firstClaims.Value;
                    if (claimValue != null)
                    {
                        return claimValue;
                    }
                }
            }
            return null;
        }
       
        public string UserId
        {
            get
            {
                var userId = GetPrincipipal();
                return (userId != null ? userId : Guid.Empty).ToString();
            }
        }
        public string UserName
        {
            get
            {
                var userName = GetPrincipipal();
                return (userName != null ? userName : UserName).ToString();
            }
        }
        // testi o9ray l message li sift lik
        //public string[] Permissions { get { return !string.IsNullOrEmpty((string)_headers?["permissions"]) ? _headers?["permissions"].ToString().Split(',') : new string[0]; } }

        public bool IsAuthenticated
        {
            get
            {
                return GetPrincipipal() != null; 
            }
        }

    }
}

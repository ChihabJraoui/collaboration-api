using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private IHeaderDictionary _headers;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _headers = httpContextAccessor.HttpContext?.Request?.Headers;
        }

        public string UserId { get { return !string.IsNullOrEmpty(_headers?["userId"]) ? (string)_headers?["userId"] : Guid.Empty.ToString(); } }

        public string[] Permissions { get { return !string.IsNullOrEmpty((string)_headers?["permissions"]) ? _headers?["permissions"].ToString().Split(',') : new string[0]; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty((string)_headers?["userId"]); } }
        
    }
}

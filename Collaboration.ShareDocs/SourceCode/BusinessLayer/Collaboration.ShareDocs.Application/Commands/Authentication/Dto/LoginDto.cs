using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Authentication.Dto
{
    public class LoginDto
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}

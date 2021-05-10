using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Common.Exceptions;

namespace Collaboration.ShareDocs.Application.Common.Services
{
    public interface IServiceHelper
    {
        bool IsEmail(string email);
        Task<(bool isValid, List<Error> errors)> ValidatePassword(string password);
    }
}

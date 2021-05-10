using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Collaboration.ShareDocs.Application.Common.Services
{
  
    public class ServiceHelper : IServiceHelper
    {
        
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceHelper(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
        
            this._mapper = mapper;
            _userManager = userManager;
        }
        public bool IsEmail(string email)
        {
            string MatchEmailPattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

            if (email != null) return Regex.IsMatch(email, MatchEmailPattern, RegexOptions.Compiled);

            else return false;
        }

        public async Task<(bool isValid, List<Error> errors)> ValidatePassword(string password)
        {
            var passwordValid  = true;
            var passwordErrors = new List<Error>();
            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager, null, password);
                if (!result.Succeeded)
                {
                    passwordValid = false;
                    passwordErrors.AddRange(result.Errors.Select(er => new Error
                    {
                        Name    = er.Code,
                        Message = er.Description
                    }));
                }
            }

            return (passwordValid, passwordErrors);
        }
    }
}

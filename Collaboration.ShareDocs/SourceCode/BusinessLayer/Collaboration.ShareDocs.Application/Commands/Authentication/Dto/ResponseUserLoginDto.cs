using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping;
using Collaboration.ShareDocs.Persistence.Entities;

namespace Collaboration.ShareDocs.Application.Commands.Authentication.Dto
{
    public class ResponseUserLoginDto : IMapForm<ApplicationUser>
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public string EmailConfirmed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, ResponseUserLoginDto>();
        }
    }
}

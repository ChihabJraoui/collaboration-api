using AutoMapper;
using Core.Entities;
using Infrastracture.Identity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.Identity.Mapping
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<Member, ApplicationUser>().ConstructUsing(u => new ApplicationUser { Id = u.MemberId.ToString()});
            CreateMap<ApplicationUser, Member>().ConstructUsing(au => new Member());
        }
    }
}

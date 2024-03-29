﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        
        public  ICollection<UserProject> Projects { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Followers { get; set; }
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Followings{ get; set; } 

        [JsonIgnore]
        public List<NotificationApplicationUser> NotificationApplicationUsers { get; set; }

        public ICollection<IndividualChat> ChatMessagings { get; set; }
        public List<Group> Groups { get; set; }

        public ApplicationUser()
        {
            Followers = new List<ApplicationUser>();
            Followings = new List<ApplicationUser>();
            ChatMessagings = new HashSet<IndividualChat>();
        }
       

    }
}

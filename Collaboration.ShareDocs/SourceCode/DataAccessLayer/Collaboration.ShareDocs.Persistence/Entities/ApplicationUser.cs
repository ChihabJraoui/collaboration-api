using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<UserProject> Projects { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Followings{ get; set; }
        public List<NotificationApplicationUser> NotificationApplicationUsers { get; set; }

    }
}

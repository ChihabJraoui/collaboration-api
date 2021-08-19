using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Group
    {
        public Guid GroupID { get; set; }
        public string  Name { get; set; }
        public Guid Owner { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public List<IndividualChat> Messages { get; set; }
        public DateTime CreatedAt { get; set; }

        public Group()
        {
            Members = new List<ApplicationUser>();
            Messages = new List<IndividualChat>();
        }


       
    }
}

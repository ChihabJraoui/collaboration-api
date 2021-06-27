using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class UserProject
    { 
        //user : book , category : project
        public Guid UserId { get; set; }

        
        public ApplicationUser User { get; set; }
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; set; }
        public bool IsAdmin { get; set; }

    }
}

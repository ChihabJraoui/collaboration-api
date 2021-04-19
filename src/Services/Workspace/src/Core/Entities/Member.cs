using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [NotMapped]
    public class Member
    {
        public Guid MemberId { get; set; }
        //
        public ICollection<ProjectsMembers> ProjectsMembers { get; set; }
       
       

    }

    
}

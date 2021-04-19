using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ProjectsMembers
    {

        public int Id { get; set; }
        public Guid MemberId { get; set; }
       // public Member Member { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

    }
}

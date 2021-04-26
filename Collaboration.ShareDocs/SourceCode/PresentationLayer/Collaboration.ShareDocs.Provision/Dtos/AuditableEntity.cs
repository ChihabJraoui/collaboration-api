using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Provision.Dtos
{
    public class AuditableEntityDto
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

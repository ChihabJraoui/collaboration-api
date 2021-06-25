using Collaboration.ShareDocs.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Workspace : AuditableEntity
    {
        public Workspace()
        {
            Projects = new Collection<Project>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Owner { get; set; }

        public bool BookMark { get; set; }

        public bool IsPrivate { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}

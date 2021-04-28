using Collaboration.ShareDocs.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Project:AuditableEntity
    {
        public Project()
        {
            Folders = new Collection<Folder>();
        }
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<Folder> Folders { get; set; } 
        public virtual Workspace Workspace { get; set; }

        //public ICollection<UserProject> Users { get; set; }
    }
}

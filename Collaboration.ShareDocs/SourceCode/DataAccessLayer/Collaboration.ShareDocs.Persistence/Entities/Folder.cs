using Collaboration.ShareDocs.Persistence.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Entities
{
    public class Folder:Component
    {
        public Guid FolderId { get; set; }
        public Folder(string name) : base(name)
        {
            Components = new List<Folder>();
            Files = new Collection<File>();
        }
        public List<Folder> Components { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<File> Files { get; set; }

    }

}

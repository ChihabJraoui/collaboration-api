using Core.Commun;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Core.Entities
{
    public class Folder:Component
    {
        public Guid FolderId { get; set; }
        public Folder(string name) : base(name)
        {
            components=new List<Folder>();
            Files = new Collection<File>();
        }
        public List<Folder> components { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<File> Files { get; set; }

       
    }
}

using Collaboration.ShareDocs.Persistence.Entities;

namespace Collaboration.ShareDocs.Persistence.Common
{
    public class Component:AuditableEntity
    {
        public string Name { get; set; }
        public Folder Parent { get; set; }
        public Component(string name)
        {
            this.Name = name;
        }

    }
}

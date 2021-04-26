using Collaboration.ShareDocs.Persistence.Entities;


namespace Collaboration.ShareDocs.Provision.Dtos
{
    public class ComponentDto : AuditableEntityDto
    {
        public string Name { get; set; }
        public FolderDto Parent { get; set; }
        public ComponentDto(string name)
        {
            this.Name = name;
        }

    }
}

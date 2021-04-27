using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Mapping; 

namespace Collaboration.ShareDocs.Application.Project.Queries.GetProjectByKeyWord
{
    public class ProjectDto : IMapForm<Persistence.Entities.Project>
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public virtual Persistence.Entities.Workspace Workspace { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Persistence.Entities.Project, ProjectDto>();
        }

    }
}

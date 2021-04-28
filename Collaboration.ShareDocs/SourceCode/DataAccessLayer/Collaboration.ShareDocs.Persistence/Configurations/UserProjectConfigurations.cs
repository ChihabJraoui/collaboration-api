using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.ShareDocs.Persistence.Configurations
{
    //class UserProjectConfigurations : IEntityTypeConfiguration<UserProject>
    //{
    //    public void Configure(EntityTypeBuilder<UserProject> modelBuilder)
    //    {

    //        modelBuilder.HasKey(bc => new { bc.UserId, bc.ProjectId });

    //        modelBuilder.HasOne(bc => bc.User)
    //                    .WithMany(b => b.Projects)
    //                    .HasForeignKey(bc => bc.UserId);

    //        modelBuilder.HasOne(bc => bc.Project)
    //                    .WithMany(c => c.Users)
    //                    .HasForeignKey(bc => bc.ProjectId);
    //    }
    //}
}

using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.ShareDocs.Persistence.Configurations
{
    //internal class FollowConfigurations : IEntityTypeConfiguration<Follow>
    //{
    //    public void Configure(EntityTypeBuilder<Follow> modelBuilder)
    //    {

    //        modelBuilder.HasKey(bc => new { bc.MemberId, bc.FollowerId });

    //        modelBuilder.HasOne(bc => bc.Member)
    //                    .WithMany(b => b.Follows)
    //                    .HasForeignKey(bc => bc.MemberId);

    //        modelBuilder.HasOne(bc => bc.Follower)
    //                    .WithMany(c => c.Follows)
    //                    .HasForeignKey(bc => bc.FollowerId);
    //    }
    //}
}

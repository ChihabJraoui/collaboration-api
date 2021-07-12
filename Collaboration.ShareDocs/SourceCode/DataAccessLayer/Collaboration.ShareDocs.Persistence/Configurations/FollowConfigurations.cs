using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.ShareDocs.Persistence.Configurations
{
    //internal class FollowConfigurations : IEntityTypeConfiguration<Follow>
    //{
    //    public void Configure(EntityTypeBuilder<Follow> modelBuilder)
    //    {

    //        modelBuilder.HasKey(bc => new { bc.FollowerId, bc.FollowingId });

    //        modelBuilder.HasOne(e => e.Following)
    //                      .WithMany(user => user.Followers)
    //                      .HasForeignKey(e => e.FollowingId)
    //                      .OnDelete(DeleteBehavior.NoAction);

    //        modelBuilder.HasOne(e => e.Follower)
    //            .WithMany(user => user.Followings)
    //            .HasForeignKey(e => e.FollowerId)
    //            .OnDelete(DeleteBehavior.NoAction);

          
    //    }
    //}
}

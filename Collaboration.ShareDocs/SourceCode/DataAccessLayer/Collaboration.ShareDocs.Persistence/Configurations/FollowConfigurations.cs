using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.ShareDocs.Persistence.Configurations
{
    internal class FollowConfigurations : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> modelBuilder)
        {

            modelBuilder.HasKey(bc => new { bc.FollowerId, bc.FollowingId });


            modelBuilder.HasOne(bc => bc.Follower)
                        .WithMany(c => c.Followers)
                        .HasForeignKey(bc => bc.FollowerId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasOne(bc => bc.following)
                                  .WithMany(c => c.Followings)
                                  .HasForeignKey(bc => bc.FollowingId)
                                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

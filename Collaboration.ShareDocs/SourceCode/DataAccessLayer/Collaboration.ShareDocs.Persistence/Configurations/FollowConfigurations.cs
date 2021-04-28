using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collaboration.ShareDocs.Persistence.Configurations
{
    internal class FollowConfigurations : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> modelBuilder)
        {

            //modelBuilder.HasKey(bc => new { bc.UserId, bc.FollowerId });

            //modelBuilder.HasMany(e => e.SubsequentStatuses)
            //            .WithOne(s => s.ParentStatuses)
            //            .HasForeignKey(e => e.ParentId);



            //modelBuilder.HasOne(bc => bc.User)
            //            .WithMany(b => b.Follows)
            //            .HasForeignKey(bc => bc.UserId);

            //modelBuilder.HasOne(bc => bc.Follower)
            //            .WithMany(c => c.Follows)
            //            .HasForeignKey(bc => bc.FollowerId);
        }
    }
}

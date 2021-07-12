﻿using Collaboration.ShareDocs.Persistence.Common;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Collaboration.ShareDocs.Persistence
{
    public class AppDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

 
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options,ICurrentUserService currentUser,IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUser;
            _dateTime = dateTime;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedBy = _currentUserService.UserId;
                        entry.Entity.DeletedAt = _dateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationApplicationUser> UserNotifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationApplicationUser>()
                    .HasKey(k => new { k.NotificationId, k.ApplicationUserId });
            modelBuilder.Entity<Follow>()
                    .HasKey(k => new { k.FollowingId, k.FollowerId});




            modelBuilder.Entity<Follow>(entity =>
            {

                entity.HasOne(d => d.Following)
                    .WithMany(p => p.Followings)
                    .HasForeignKey(d => d.FollowingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Followers)
                    .HasForeignKey(d => d.FollowerId);
            });
           

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            //// Entity Relation configuration
            var configuration = new IdentityConfigurations();
            base.OnModelCreating(modelBuilder);
            // Entity Relation configuration
            configuration.RenameIdentityTables(modelBuilder);
            configuration.IdentityUserLoginConfig(modelBuilder);
        }

    }
}

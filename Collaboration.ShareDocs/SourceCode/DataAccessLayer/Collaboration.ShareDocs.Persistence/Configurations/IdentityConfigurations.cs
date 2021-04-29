﻿using System;
using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.ShareDocs.Persistence.Configurations
{
    public class IdentityConfigurations
    {
        public void RenameIdentityTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Users");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("UserTokens");
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("UserRoles");
            });

        }
        public void IdentityUserLoginConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(m => m.LoginProvider);
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(m => m.ProviderKey);
        }
    }
}
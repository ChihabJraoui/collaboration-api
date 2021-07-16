using Collaboration.ShareDocs.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence.Configurations
{
    public class IndividualChatConfiguration : IEntityTypeConfiguration<IndividualChat>
    {
        public void Configure(EntityTypeBuilder<IndividualChat> modelBuilder)
        {

            

            modelBuilder.HasOne(bc => bc.To)
                        .WithMany(b => b.ChatMessagings)
                        .HasForeignKey(bc => bc.UserId);

           
        }
    }
}

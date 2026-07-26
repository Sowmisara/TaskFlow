using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Models;

namespace TaskFlow.Infrastructure.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Property(i => i.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(i => i.Description).HasMaxLength(500);

            builder.Property(i => i.Status).HasConversion<string>().HasMaxLength(20);

            builder.Property(i => i.Priority).HasConversion<string>().HasMaxLength(20);

            builder.HasOne(i => i.Project).WithMany(p => p.Issues).HasForeignKey(i => i.ProjectId).OnDelete(DeleteBehavior.Restrict);

            // --- Project relationship: CASCADE (alternative — comment out one) ---
            // builder.HasOne(i => i.Project)
            //        .WithMany(p => p.Issues)
            //        .HasForeignKey(i => i.ProjectId)
            //        .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.AssignedToUser).WithMany().HasForeignKey(i => i.AssignedToUserId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

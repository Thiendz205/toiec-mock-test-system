using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Entities;

namespace ToeicMockTest.Infrastructure.Persistence.Configurations
{
    public class QuestionUpdateHistoryConfiguration : IEntityTypeConfiguration<QuestionUpdateHistory>
    {
        public void Configure(EntityTypeBuilder<QuestionUpdateHistory> builder)
        {
            builder.ToTable("QuestionUpdateHistories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OldContent).HasMaxLength(4000);
            builder.Property(x => x.NewContent).HasMaxLength(4000);

            builder.HasOne(x => x.Question)
                   .WithMany(q => q.UpdateHistories)
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UpdatedBy)
                    .WithMany(u => u.QuestionUpdateHistories) 
                    .HasForeignKey(x => x.UpdatedById)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

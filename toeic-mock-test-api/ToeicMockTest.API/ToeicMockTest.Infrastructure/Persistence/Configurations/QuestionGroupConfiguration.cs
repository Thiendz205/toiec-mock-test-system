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
    public class QuestionGroupConfiguration : IEntityTypeConfiguration<QuestionGroup>
    {
        public void Configure(EntityTypeBuilder<QuestionGroup> builder)
        {
            builder.ToTable("QuestionGroups");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content).HasMaxLength(2000);
            builder.Property(x => x.AudioUrl).HasMaxLength(500);
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
            builder.Property(x => x.Transcript).HasMaxLength(4000);

            builder.Property(x => x.Part).IsRequired();

            // Audit
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

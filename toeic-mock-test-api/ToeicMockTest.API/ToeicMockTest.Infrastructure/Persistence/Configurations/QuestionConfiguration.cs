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
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(x => x.Explanation)
                   .HasMaxLength(2000);

            // Audit
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasOne(x => x.Difficulty)
                   .WithMany()
                   .HasForeignKey(x => x.DifficultyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.QuestionGroup)
                   .WithMany(g => g.Questions)
                   .HasForeignKey(x => x.QuestionGroupId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.CreatedBy)
                   .WithMany()
                   .HasForeignKey(x => x.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

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
    public class QuestionFeedbackConfiguration : IEntityTypeConfiguration<QuestionFeedback>
    {
        public void Configure(EntityTypeBuilder<QuestionFeedback> builder)
        {

            builder.ToTable("QuestionFeedbacks");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IsResolved).HasDefaultValue(false);

            // Quan hệ
            builder.HasOne(x => x.Question)
                   .WithMany()
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

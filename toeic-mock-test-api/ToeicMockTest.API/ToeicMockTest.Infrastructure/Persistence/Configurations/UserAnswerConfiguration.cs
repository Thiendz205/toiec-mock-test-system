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
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.ToTable("UserAnswers");
            builder.HasKey(ua => ua.Id);

            builder.HasOne(ua => ua.ExamAttempt)
                .WithMany(ea => ea.UserAnswers) 
                .HasForeignKey(ua => ua.ExamAttemptId) 
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ua => ua.Question)
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ua => ua.SelectedAnswer)
                .WithMany()
                .HasForeignKey(ua => ua.SelectedAnswerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(ua => ua.IsCorrect).IsRequired();

            builder.HasIndex(ua => new { ua.ExamAttemptId, ua.QuestionId }).IsUnique();
        }
    }
}

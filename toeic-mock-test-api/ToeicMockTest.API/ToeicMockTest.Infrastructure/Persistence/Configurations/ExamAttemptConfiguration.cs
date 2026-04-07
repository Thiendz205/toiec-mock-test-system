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
    public class ExamAttemptConfiguration : IEntityTypeConfiguration<ExamAttempt>
    {
        public void Configure(EntityTypeBuilder<ExamAttempt> builder)
        {
            builder.ToTable("ExamAttempts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartedAt).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            // Quan hệ với Exam và User
            builder.HasOne(x => x.Exam)
                   .WithMany()
                   .HasForeignKey(x => x.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình các trường điểm số
            builder.Property(x => x.TotalScore).HasDefaultValue(0);
        }
    }
}

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
    public class ExamAttemptPartStatConfiguration : IEntityTypeConfiguration<ExamAttemptPartStat>
    {
        public void Configure(EntityTypeBuilder<ExamAttemptPartStat> builder)
        {
            builder.ToTable("ExamAttemptPartStats");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Part).IsRequired();
            builder.Property(x => x.CorrectCount).IsRequired();

            // Quan hệ với ExamAttempt
            builder.HasOne(x => x.ExamAttempt)
                   .WithMany() // Nếu bên ExamAttempt không có List Stat thì để trống WithMany()
                   .HasForeignKey(x => x.ExamAttemptId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

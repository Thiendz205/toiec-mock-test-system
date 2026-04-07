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
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exams");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(x => x.DurationMinutes)
                   .IsRequired();

            // Cấu hình Audit
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            // Quan hệ 1-N với ExamCollection (Nullable)
            builder.HasOne(x => x.ExamCollection)
                   .WithMany(c => c.Exams)
                   .HasForeignKey(x => x.ExamCollectionId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

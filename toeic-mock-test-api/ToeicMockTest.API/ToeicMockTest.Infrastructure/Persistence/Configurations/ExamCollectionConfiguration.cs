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
    public class ExamCollectionConfiguration : IEntityTypeConfiguration<ExamCollection>
    {
        public void Configure(EntityTypeBuilder<ExamCollection> builder)
        {
            builder.ToTable("ExamCollections");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Description)
                   .HasMaxLength(1000);

            builder.Property(x => x.ImageUrl)
                   .HasMaxLength(500);

            // Audit properties
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            // Quan hệ 1-N: Một Collection có nhiều Exams
            builder.HasMany(x => x.Exams)
                   .WithOne(e => e.ExamCollection)
                   .HasForeignKey(e => e.ExamCollectionId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

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
    public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder.ToTable("ExamQuestions");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.ExamId, x.QuestionId }).IsUnique();

            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.Score).IsRequired();

            builder.HasOne(x => x.Exam)
                   .WithMany(e => e.ExamQuestions)
                   .HasForeignKey(x => x.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                   .WithMany()
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

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
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasMaxLength(2000); 

            builder.Property(x => x.IsCorrect)
                   .IsRequired()
                   .HasDefaultValue(false); 

            builder.Property(x => x.Order)
                   .IsRequired();


            builder.HasOne(x => x.Question)
                   .WithMany(q => q.Answers)
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

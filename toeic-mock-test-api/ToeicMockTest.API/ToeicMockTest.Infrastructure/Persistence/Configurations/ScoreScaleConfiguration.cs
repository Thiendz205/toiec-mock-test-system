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
    public class ScoreScaleConfiguration : IEntityTypeConfiguration<ScoreScale>
    {
        public void Configure(EntityTypeBuilder<ScoreScale> builder)
        {
            builder.ToTable("ScoreScales");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Exam)
                   .WithMany()
                   .HasForeignKey(x => x.ExamId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

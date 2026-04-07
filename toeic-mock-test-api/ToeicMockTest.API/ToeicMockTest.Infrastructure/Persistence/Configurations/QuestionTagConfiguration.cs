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
    public class QuestionTagConfiguration : IEntityTypeConfiguration<QuestionTag>
    {
        public void Configure(EntityTypeBuilder<QuestionTag> builder)
        {
            builder.ToTable("QuestionTags");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.QuestionId, x.TagId }).IsUnique();

            builder.HasOne(x => x.Question)
                   .WithMany(q => q.QuestionTags)
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tag)
                   .WithMany(t => t.QuestionTags)
                   .HasForeignKey(x => x.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

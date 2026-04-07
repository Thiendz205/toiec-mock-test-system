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

    public class UserVerificationTokenConfiguration : IEntityTypeConfiguration<UserVerificationToken>
    {
        public void Configure(EntityTypeBuilder<UserVerificationToken> builder)
        {
            builder.ToTable("UserVerificationTokens");

            builder.HasKey(uvt => uvt.Id);

            builder.Property(uvt => uvt.TokenValue)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(uvt => uvt.Type)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(uvt => uvt.ExpiryDate)
                .IsRequired();

            builder.HasOne(uvt => uvt.User)
                .WithMany()
                .HasForeignKey(uvt => uvt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(uvt => uvt.TokenValue);
        }
    }
}
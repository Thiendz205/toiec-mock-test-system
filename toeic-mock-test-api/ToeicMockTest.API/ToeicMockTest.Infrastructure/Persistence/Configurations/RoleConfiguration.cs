using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common.Constants;
using ToeicMockTest.Domain.Entities;

namespace ToeicMockTest.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Description).HasMaxLength(255);

            builder.HasData(
               Role.CreateSystemRole(
                   RoleConstants.AdminId,
                   RoleConstants.Admin,
                   "System Administrator with full access"
               ),
               Role.CreateSystemRole(
                   RoleConstants.StaffId,
                   RoleConstants.Staff,
                   "Content Manager responsible for exams and questions"
               ),
               Role.CreateSystemRole(
                   RoleConstants.UserId,
                   RoleConstants.User,
                   "Standard User for taking mock tests and viewing results"
               )
           );
        }
    }
}

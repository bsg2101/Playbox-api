using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PlayBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.RefreshToken).HasMaxLength(256);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}

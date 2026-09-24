using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreMinimalApi.Data.Persistency.Configs
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasOne(t => t.User).WithMany(u=>u.RefreshTokens).HasForeignKey(t => t.UserId);
            builder.Property(t => t.Token).HasMaxLength(200);
        }
    }
}
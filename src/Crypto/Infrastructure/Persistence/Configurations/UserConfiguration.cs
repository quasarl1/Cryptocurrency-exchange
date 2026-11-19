using System.Collections.Immutable;
using CryptoExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CryptoExchange.Data.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //general Key

        builder.HasKey(u => u.Id);


        // Unique Tag/Email for removing coincidences
        builder.HasIndex(u => u.Tag).IsUnique();
        //Tag can be changed once every 3 weeks
        builder.HasIndex(u => u.Email).IsUnique();  
        builder.HasOne<AuthUser>().WithOne().HasForeignKey<AuthUser>(a => a.UserId);
    }
}
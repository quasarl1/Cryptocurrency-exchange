using CryptoExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoExchange.Data.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //general Key

        builder.HasKey(u => u.Id);


        // Unique Email for removing coincidences

        builder.HasIndex(u => u.Email).IsUnique();  
    }
}
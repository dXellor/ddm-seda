using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using seda_dll.Models;

namespace seda_dll.Data.Configurations;

public class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);  
        builder.HasAlternateKey(u => u.Email);

        builder.Property(x => x.Id).HasColumnName("user_id").ValueGeneratedOnAdd();
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(x => x.LastName).HasColumnName("last_name").IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
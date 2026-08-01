using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasKey(x => x.Id);


        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);


        builder.Property(x => x.PasswordHash)
            .IsRequired();


        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);

    }
}
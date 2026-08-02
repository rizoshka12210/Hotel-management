using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class HotelConfiguration 
    : IEntityTypeConfiguration<Hotel>
{

    public void Configure(EntityTypeBuilder<Hotel> builder)
    {

        builder.HasKey(x => x.Id);


        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);


        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(250);


    }
}
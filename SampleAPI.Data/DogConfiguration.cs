using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleAPI.Data;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.HasKey(dog => dog.Name);

        builder.Property(dog => dog.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(dog => dog.TailLength)
            .IsRequired();

        builder.Property(dog => dog.Weight)
            .IsRequired();
        
        builder.Property(dog => dog.Color)
            .IsRequired()
            .HasMaxLength(50);
    }
}
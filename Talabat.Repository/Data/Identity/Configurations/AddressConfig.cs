using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Data.Identity.Configurations
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(a => a.User)
                   .WithOne(u => u.Address)
                   .HasForeignKey<Address>(a => a.AppUserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
            builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        }
    }
}

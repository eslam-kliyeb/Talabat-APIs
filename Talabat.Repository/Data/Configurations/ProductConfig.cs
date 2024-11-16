using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P => P.ProductBrandId);
            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(P => P.ProductTypeId);
            builder.Property(P=>P.Name).IsRequired().HasMaxLength(50);
            builder.Property(P=> P.Description).IsRequired();
            builder.Property(P=>P.PictureUrl).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
        }
    }
}

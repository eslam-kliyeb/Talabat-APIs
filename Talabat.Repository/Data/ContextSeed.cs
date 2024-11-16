using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class ContextSeed
    {
        // Seeding
        public static async Task SeedAsync(ApplicationDbContext dataContext)
        {
            if (!dataContext.ProductBrands.Any())
            {
                var BrandsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dataContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await dataContext.SaveChangesAsync();
                }
            }
            if (!dataContext.ProductTypes.Any())
            {
                var TypesData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dataContext.Set<ProductType>().AddAsync(Type);
                    }
                    await dataContext.SaveChangesAsync();
                }
            }
            if (!dataContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dataContext.Set<Product>().AddAsync(Product);
                    }
                    await dataContext.SaveChangesAsync();
                }
            }
            if(!dataContext.DeliveryMethods.Any())
            {
                var DeliveryData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/delivery.json");
                var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
                if (Delivery?.Count>0)
                {
                    foreach (var Product in Delivery)
                    {
                        await dataContext.Set<DeliveryMethod>().AddAsync(Product);
                    }
                    await dataContext.SaveChangesAsync();
                }
            }
        }
    }
}

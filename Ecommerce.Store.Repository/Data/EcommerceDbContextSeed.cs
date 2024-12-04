using Ecommerce.Store.Core.Entities;
using Ecommerce.Store.Core.Entities.Order;
using Ecommerce.Store.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Store.Repository.Data
{
    public static class EcommerceDbContextSeed
    {
        public async static Task SeedAsync(EcommerceDbContext _context)
        {
            if (_context.Brands.Count() == 0)
            {
                // Brand
                //1. Read The Data From JSON File
                var brandsData = File.ReadAllText(@"..\Ecommerce.Store.Repository\Data\DataSeed\brands.json");
                // 2. Convert JSON String To List<T>

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                // 3. Seed Data To DataBase
                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                }

            }

            if (_context.Types.Count() == 0)
            {
                // Brand
                //1. Read The Data From JSON File
                var typesData = File.ReadAllText(@"..\Ecommerce.Store.Repository\Data\DataSeed\types.json");
                // 2. Convert JSON String To List<T>

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                // 3. Seed Data To DataBase
                if (types is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                }

            }

            if (_context.Products.Count() == 0)
            {
                // Brand
                //1. Read The Data From JSON File
                var productsData = File.ReadAllText(@"..\Ecommerce.Store.Repository\Data\DataSeed\products.json");
                // 2. Convert JSON String To List<T>

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                // 3. Seed Data To DataBase
                if (products is not null && products.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }

            }

            if (_context.DeliveryMethods.Count() == 0)
            {
                // Brand
                //1. Read The Data From JSON File
                var deliveryData = File.ReadAllText(@"..\Ecommerce.Store.Repository\Data\DataSeed\delivery.json");
                // 2. Convert JSON String To List<T>

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                // 3. Seed Data To DataBase
                if (deliveryMethods is not null && deliveryMethods.Count() > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                }

            }


            await _context.SaveChangesAsync();

        }
    }
}

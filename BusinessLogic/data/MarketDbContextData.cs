using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.data
{
    public class MarketDbContextData
    {
        public static async Task LoadInitialDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Brands.Any())
                {
                    var brandData = File.ReadAllText("../BusinessLogic/data/InitialData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<Brand>>(brandData);

                    foreach(var brand in brands)
                        await context.Brands.AddAsync(brand);

                    await context.SaveChangesAsync();
                }

                if (!context.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../BusinessLogic/data/InitialData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach (var category in categories)
                        await context.Categories.AddAsync(category);

                    await context.SaveChangesAsync();
                }


                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../BusinessLogic/data/InitialData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                        await context.Products.AddAsync(product);

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MarketDbContextData>();
                logger.LogError(ex.Message);
            }
        }
    }
}

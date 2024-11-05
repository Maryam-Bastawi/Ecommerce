using store4.Repository.Data.Contexts;
using Store4.core.Entities;
using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace store4.Repository.Data
{
	public static class StoreDbContextSeed
	{
		public static async Task SeedAsync(StoreDbContext dbContext)
		{
			if (dbContext.ProductBrands.Count() == 0)
			{
				//brand
				//1.read data from json file
				var BrandsData = File.ReadAllText(@"..\store4.Repository\Data\DataSeed\brands.json");
				//convert json string to list 
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
				//seed data to data base
				if (brands is not null && brands.Count() > 0)
				{
					await dbContext.ProductBrands.AddRangeAsync(brands);
					await dbContext.SaveChangesAsync();
				}
			}
			if (dbContext.ProductTypes.Count() == 0)
			{

				//1.read data from json file
				var typesData = File.ReadAllText(@"..\store4.Repository\Data\DataSeed\types.json");
				//convert json string to list 
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
				//seed data to data base
				if (types is not null && types.Count() > 0)
				{
					await dbContext.ProductTypes.AddRangeAsync(types);
					await dbContext.SaveChangesAsync();
				}
			}
			if (dbContext.Products.Count() == 0)
			{

				//1.read data from json file
				var productsData = File.ReadAllText(@"..\store4.Repository\Data\DataSeed\products.json");
				//convert json string to list 
				var product = JsonSerializer.Deserialize<List<Product>>(productsData);
				//seed data to data base
				if (product is not null && product.Count() > 0)
				{
					await dbContext.Products.AddRangeAsync(product);
					await dbContext.SaveChangesAsync();
				}
			}
			if (dbContext.deliveryMethods.Count() == 0)
			{

				//1.read data from json file
				var deliveryData = File.ReadAllText(@"..\store4.Repository\Data\DataSeed\delivery.json");
				//convert json string to list 
				var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
				//seed data to data base
				if (deliveryMethods is not null && deliveryMethods.Count() > 0)
				{
					await dbContext.deliveryMethods.AddRangeAsync(deliveryMethods);
					await dbContext.SaveChangesAsync();
				}
			}



		}

	}
}

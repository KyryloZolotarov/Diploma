using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogTypes.Any())
        {
            await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogSubTypes.Any())
        {
            await context.CatalogSubTypes.AddRangeAsync(GetPreconfiguredCatalogSubTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogModels.Any())
        {
            await context.CatalogModels.AddRangeAsync(GetPreconfiguredCatalogModels());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>()
        {
            new CatalogBrand() { Brand = "Honda" },
            new CatalogBrand() { Brand = "Mazda" },
            new CatalogBrand() { Brand = "Toyota" },
            new CatalogBrand() { Brand = "Volkswagen" },
            new CatalogBrand() { Brand = "BMW" }
        };
    }

    private static IEnumerable<CatalogSubType> GetPreconfiguredCatalogSubTypes()
    {
        return new List<CatalogSubType>()
        {
            new CatalogSubType() { SubType = "Engine Belts", CatalogTypeId = 1 },
            new CatalogSubType() { SubType = "Engine Block", CatalogTypeId = 1 },
            new CatalogSubType() { SubType = "Front", CatalogTypeId = 4 },
            new CatalogSubType() { SubType = "Back", CatalogTypeId = 4 },
            new CatalogSubType() { SubType = "Body Electronics", CatalogTypeId = 3 }
        };
    }

    private static IEnumerable<CatalogModel> GetPreconfiguredCatalogModels()
    {
        return new List<CatalogModel>()
        {
            new CatalogModel() { Model = "3", CatalogBrandId = 1 },
            new CatalogModel() { Model = "5", CatalogBrandId = 5 },
            new CatalogModel() { Model = "Corolla", CatalogBrandId = 3 },
            new CatalogModel() { Model = "Camry", CatalogBrandId = 3 },
            new CatalogModel() { Model = "Jetta", CatalogBrandId = 4 }
        };
    }

    private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>()
        {
            new CatalogType() { Type = "Suspention" },
            new CatalogType() { Type = "Engine" },
            new CatalogType() { Type = "Electronics" },
            new CatalogType() { Type = "Body Parts" }
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogSubTypeId = 2, CatalogModelId = 2, AvailableStock = 100, Description = "some description", Name = ".NET Bot Black Hoodie", Price = 19.5M, PictureFileName = "1.png" },
            new CatalogItem { CatalogSubTypeId = 1, CatalogModelId = 2, AvailableStock = 100, Description = "some description", Name = ".NET Black & White Mug", Price = 8.50M, PictureFileName = "2.png" },
            new CatalogItem { CatalogSubTypeId = 2, CatalogModelId = 5, AvailableStock = 100, Description = "some description", Name = "Prism White T-Shirt", Price = 12, PictureFileName = "3.png" },
            new CatalogItem { CatalogSubTypeId = 2, CatalogModelId = 2, AvailableStock = 100, Description = "some description", Name = ".NET Foundation T-shirt", Price = 12, PictureFileName = "4.png" },
            new CatalogItem { CatalogSubTypeId = 3, CatalogModelId = 5, AvailableStock = 100, Description = "some description", Name = "Roslyn Red Sheet", Price = 8.5M, PictureFileName = "5.png" },
            new CatalogItem { CatalogSubTypeId = 2, CatalogModelId = 2, AvailableStock = 100, Description = "some description", Name = ".NET Blue Hoodie", Price = 12, PictureFileName = "6.png" },
            new CatalogItem { CatalogSubTypeId = 2, CatalogModelId = 5, AvailableStock = 100, Description = "some description", Name = "Roslyn Red T-Shirt", Price = 12, PictureFileName = "7.png" },
            new CatalogItem { CatalogSubTypeId = 2, CatalogModelId = 5, AvailableStock = 100, Description = "some description", Name = "Kudu Purple Hoodie", Price = 8.5M, PictureFileName = "8.png" },
            new CatalogItem { CatalogSubTypeId = 1, CatalogModelId = 5, AvailableStock = 100, Description = "some description", Name = "Cup<T> White Mug", Price = 12, PictureFileName = "9.png" },
            new CatalogItem { CatalogSubTypeId = 3, CatalogModelId = 2, AvailableStock = 100, Description = "some description", Name = ".NET Foundation Sheet", Price = 12, PictureFileName = "10.png" },
            new CatalogItem { CatalogSubTypeId = 3, CatalogModelId = 2, AvailableStock = 100, Description = "some description", Name = "Cup<T> Sheet", Price = 8.5M, PictureFileName = "11.png" },
            new CatalogItem { CatalogSubTypeId = 2, CatalogModelId = 5, AvailableStock = 100, Description = "some description", Name = "Prism White TShirt", Price = 12, PictureFileName = "12.png" },
        };
    }
}
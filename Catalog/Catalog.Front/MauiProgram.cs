using Catalog.Front.Helpers;
using Catalog.Front.Helpers.Interfaces;
using Catalog.Front.Repositories;
using Catalog.Front.Repositories.Intefaces;
using Catalog.Front.Services;
using Catalog.Front.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Catalog.Front
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<IHttpClientHelper, HttpClientHelper>();

            builder.Services.AddSingleton<Pages.ExistingItems>();
            builder.Services.AddSingleton<Pages.AddItem>();
            builder.Services.AddSingleton<Pages.SinglItem>();
            builder.Services.AddTransient<ICatalogItemService, CatalogItemService>();
            builder.Services.AddTransient<ICatalogItemRepository, CatalogItemRepository>();
            builder.Services.AddTransient<ICatalogBrandService, CatalogBrandService>();
            builder.Services.AddTransient<ICatalogBrandRepository, CatalogBrandRepository>();
            builder.Services.AddTransient<ICatalogTypeService, CatalogTypeService>();
            builder.Services.AddTransient<ICatalogTypeRepository, CatalogTypeRepository>();
            builder.Services.AddTransient<ICatalogSubTypeRepository, CatalogSubTypeRepository>();
            builder.Services.AddTransient<ICatalogModelRepository, CatalogModelRepository>();
            builder.Services.AddTransient<ICatalogModelService, CatalogModelService>();
            builder.Services.AddTransient<ICatalogSubTypeService, CatalogSubTypeService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
using eShop.Catalog.Extensions;
using eShop.Catalog.Services;
using eShop.Catalog.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<CatalogContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDB")));

builder.Services
    .AddMigration<CatalogContext, CatalogContextSeed>();

builder.Services
    .AddScoped<ProductService>()
    .AddScoped<ProductTypeService>()
    .AddScoped<BrandService>()
    .AddScoped<ImageStorage>();

builder.Services
    .AddGraphQLServer()
    .AddCatalogTypes()
    .AddGraphQLConventions();

var app = builder.Build();

app.MapGraphQL();

app.MapImageRoute();

app.RunWithGraphQLCommands(args);


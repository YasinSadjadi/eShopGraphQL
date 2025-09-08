using eShop.Catalog.Extention;
using eShop.Catalog.Types.Filtering;
using eShop.Catalog.Types.Sorting;
using HotChocolate.Data.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<CatalogContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDB")));

builder.Services
    .AddMigration<CatalogContext, CatalogContextSeed>();

builder.Services.AddScoped<Query>();

builder.Services
    .AddGraphQLServer()
    .AddCatalogTypes()
    .AddGraphQLConventions();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);


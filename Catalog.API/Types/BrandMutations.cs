using eShop.Catalog.Services;

namespace eShop.Catalog.Types;

[MutationType]
public static class BrandMutations
{
    public static async Task<Brand> CreateBrandAsync(
        string name,
        BrandService service,
        CancellationToken ct)
    {
        var brand = new Brand() { Name = name };
        await service.CreateBrandAsync(brand, ct);
        return brand;
    }
}
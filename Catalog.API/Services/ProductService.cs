using HotChocolate.Pagination;

namespace eShop.Catalog.Services;

public sealed class ProductService(
    CatalogContext context,
    IProductByIdDataLoader productById,
    IProductsByBrandIdDataLoader productsByBrandId,
    IProductsByTypeIdDataLoader productsByTypeId)
{
    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
        => await productById.LoadAsync(id, cancellationToken);

    public async Task<Page<Product>> GetProductsAsync(
        ProductFilter? productFilter,
        PagingArguments pagingArguments,
        CancellationToken cancellationToken = default)
    {
        var query = context.Products.AsNoTracking();

        if (productFilter?.BrandIds is { Count: > 0 } brandIds)
        {
            query = query.Where(p => brandIds.Contains(p.BrandId));
        }

        if (productFilter?.TypeIds is { Count: > 0 } typeIds)
        {
            query = query.Where(p => typeIds.Contains(p.TypeId));
        }

        return await query.OrderBy(t => t.Name).ThenBy(t => t.Id).ToPageAsync(pagingArguments, cancellationToken);
    }

    public async Task<Page<Product>> GetProductsByBrandAsync(
        int brandId,
        PagingArguments args,
        CancellationToken ct = default)
        => await productsByBrandId.LoadAsync(new PageKey<int>(brandId, args), ct);

    public async Task<Page<Product>> GetProductsByTypeAsync(
        int typeId,
        PagingArguments args,
        CancellationToken ct = default)
        => await productsByTypeId.LoadAsync(new PageKey<int>(typeId, args), ct);

    public async Task CreateProductAsync(Product product, CancellationToken ct)
    {

        if (string.IsNullOrEmpty(product.Name))
        {
            ArgumentException.ThrowIfNullOrEmpty(product.Name);
        }

        if (product.RestockThreshold >= product.MaxStockThreshold)
        {
            throw new MaxStockThresholdToSmallException(product.RestockThreshold, product.MaxStockThreshold);
        }

        if (await context.Brands.AnyAsync(t => t.Id == product.BrandId, ct))
        {
            throw new EntityNotFoundException(nameof(Brand), product.BrandId);
        }

        if (await context.ProductTypes.AnyAsync(t => t.Id == product.TypeId, ct))
        {
            throw new EntityNotFoundException(nameof(ProductType), product.TypeId);
        }



        context.Products.Add(product);
        await context.SaveChangesAsync(ct);
    }
}

public class EntityNotFoundException(string entityName, int entityId) : Exception
{
    public int EntityId { get; } = entityId;
    public string EntityName { get; } = entityName;
}

public class MaxStockThresholdToSmallException(int restockThresold, int maxStockThreshold) : Exception
{
    public int RestockThresold { get; } = restockThresold;
    public int MaxStockThreshold { get; } = maxStockThreshold;
}
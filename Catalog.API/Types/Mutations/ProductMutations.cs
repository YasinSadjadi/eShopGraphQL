using eShop.Catalog.Services.Services;

namespace eShop.Catalog.Types.Mutations;

[MutationType]
public static class ProductMutations
{
    [Error<InvalidBrandIdErrorFactory>]
    [Error<InvalidProductTypeIdError>]
    [Error<EntityNotFoundException>]
    [Error<ArgumentException>]
    [Error<MaxStockThresholdToSmallException>]
    public static async Task<Product> CreateProductAsync(
        CreateProductInput input,
        ProductService service,
        CancellationToken ct
        )
    {
        var product = new Product
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            BrandId = input.BrandId,
            TypeId = input.TypeId,
            RestockThreshold = input.RestockThreshold,
            MaxStockThreshold = input.MaxStockThreshold
        };

        await service.CreateProductAsync(product, ct);
        return default!;
    }
}


public class InvalidBrandIdErrorFactory : IPayloadErrorFactory<EntityNotFoundException, InvalidBrandIdError?>
{
    public InvalidBrandIdError? CreateErrorFrom(EntityNotFoundException exception)
    {
        if (exception.EntityName == nameof(Brand))
            return new InvalidBrandIdError(exception.EntityId);

        return null;
    }
}

public record InvalidBrandIdError([property: ID<Brand>]int id)
{
    public string Message => "The provided brand id is invalid";
}

public record InvalidProductTypeIdError([property: ID<ProductType>] int id)
{
    public string Message => "The provided product type id is invalid";
    public static InvalidProductTypeIdError? CreateErrorFrom(EntityNotFoundException exception)
    {
        if (exception.EntityName == nameof(ProductType))
            return new InvalidProductTypeIdError(exception.EntityId);

        return null;
    }
}

public record CreateProductInput(
    string Name,
    string? Description,
    decimal Price,
    [ID<Brand>] int BrandId,
    [ID<ProductType>] int TypeId,
    int RestockThreshold,
    int MaxStockThreshold);

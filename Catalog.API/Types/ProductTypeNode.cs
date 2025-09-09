using eShop.Catalog.Services;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[ObjectType<Models.ProductType>]
public static partial class ProductTypeNode
{
    [UsePaging]
    public static async Task<Connection<Product>> GetProductsAsync(
        [Parent] Models.ProductType productType,
        PagingArguments pagingArgs,
        ProductService productService,
        CancellationToken cancellationToken)
        => await productService.GetProductsByTypeAsync(productType.Id, pagingArgs, cancellationToken).ToConnectionAsync();
}
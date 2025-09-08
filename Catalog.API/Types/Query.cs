using eShop.Catalog.Types.Filtering;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace eShop.Catalog.Types;

public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Brand> GetBrands(CatalogContext context)
    {
        return context.Brands.AsQueryable();
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Brand> GetBrandById(int id, CatalogContext context)
    {
        return context.Brands.Where(t => t.Id == id);
    }


    [UsePaging(DefaultPageSize = 10, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering<ProductFilterInputType>]
    [UseSorting]
    public IQueryable<Product> GetProducts(CatalogContext context, IFilterContext filterContext, ISortingContext sortingContext)
    {
        filterContext.Handled(false);
        sortingContext.Handled(false);

        IQueryable<Product> query = context.Products;

        if (!filterContext.IsDefined)
            query = context.Products.Where(p => p.BrandId == 1);

        if (!sortingContext.IsDefined)
            query = context.Products.OrderBy(t => t.Brand!.Name)
                .ThenByDescending(t => t.Price);

        return query;

    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Product> GetProductById(int id, CatalogContext context)
    {
        return context.Products.Where(p => p.Id == id);
    }

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    public IQueryable<ProductType> GetProductTypes(CatalogContext context)
    {
        return context.ProductTypes.AsQueryable();
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductType> GetProductTypeById(int id, CatalogContext context)
    {
        return context.ProductTypes.Where(t => t.Id == id);
    }
}



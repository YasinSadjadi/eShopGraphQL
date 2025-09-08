using HotChocolate.Data.Filters;

namespace eShop.Catalog.Types.Filtering
{
    public class ProductFilterInputType : FilterInputType<Product>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Product> descriptor)
        {
            //base.Configure(descriptor);
            descriptor.BindFieldsExplicitly();

            descriptor.Field(p => p.Name).Type<SearchStringOperationFilterInputType>();
            descriptor.Field(p => p.Type);
            descriptor.Field(p => p.Brand);
            descriptor.Field(p => p.Price);
            descriptor.Field(p => p.AvailableStock);
        }
    }
}

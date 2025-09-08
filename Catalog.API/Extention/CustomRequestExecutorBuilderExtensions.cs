using eShop.Catalog.Types.Filtering;
using HotChocolate.Execution.Configuration;

namespace eShop.Catalog.Extention
{
    public static class CustomRequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder AddGraphQLConventions(this IRequestExecutorBuilder builder)
        {
            builder.AddProjections()
                .AddFiltering(
                c => c.AddDefaults()
                .BindRuntimeType<string, DefaultStringOperationFilterInputType>()
                )
                .AddSorting();
            return builder;
        }
    }
}

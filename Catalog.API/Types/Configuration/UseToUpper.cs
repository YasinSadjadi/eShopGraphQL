using HotChocolate.Types.Descriptors;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace eShop.Catalog.Types.Configuration;

public static class UseToUpper
{
    public static IObjectFieldDescriptor UseToUpperField(this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Use(next => async context =>
        {
            await next(context);
            if (context.Result is string s)
            {
                context.Result = s.ToUpperInvariant();
            }
        });
    }
}
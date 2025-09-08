using HotChocolate;
using HotChocolate.Types;

[assembly: Module("ShippingTypes")]

namespace eShop.Shipping.Types;

[QueryType]
public static class Foo
{
    public static string hello() => "hello";
}

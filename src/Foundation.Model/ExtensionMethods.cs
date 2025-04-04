using OneOf;
using OneOf.Types;

namespace Foundation.Model;

public static class ExtensionMethods
{
    public static OneOf<T, None> ToOption<T>(this T? value) where T : struct => value.HasValue ? value.Value : new None();
    
    public static OneOf<string, None> ToOption(this string? value) => value != null ? value : new None();
}
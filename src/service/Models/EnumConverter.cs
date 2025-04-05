namespace GalaxyMapSiteApi.Models;

public class EnumConverter
{
    public static TEnum ConvertToEnumOrDefault<TEnum>(string? value, TEnum defaultValue)
        where TEnum : Enum
    {
        if (Enum.TryParse(typeof(TEnum), value, out var result))
        {
            return (TEnum)result;
        }
        return defaultValue;
    }

    public static TEnum? ConvertToEnumOrNull<TEnum>(string? value)
        where TEnum : struct, Enum
    {
        if (Enum.TryParse(typeof(TEnum), value, out var result))
        {
            return (TEnum)result;
        }
        return null;
    }
}

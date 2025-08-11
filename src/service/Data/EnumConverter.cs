using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class EnumConverter<TEnum> : ValueConverter<TEnum, string>
    where TEnum : struct, Enum
{
    public EnumConverter()
        : base(
            enumValue => enumValue.ToString(),
            stringValue => EnumConverter.ConvertToEnumOrDefault<TEnum>(stringValue)
        ) { }
}

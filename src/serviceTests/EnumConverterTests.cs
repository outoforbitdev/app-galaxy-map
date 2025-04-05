using GalaxyMapSiteApi.Models;

namespace GalaxyMapSiteApi.Tests;

public class EnumConverterTests
{
    [Fact]
    public void ConvertToEnumOrDefault_ValidInput_Enum()
    {
        SpacelaneStartReason actual = EnumConverter.ConvertToEnumOrDefault(
            "Created",
            SpacelaneStartReason.Discovered
        );
        Assert.Equal(SpacelaneStartReason.Created, actual);
    }

    [Fact]
    public void ConvertToEnumOrDefault_InvalidInput_Default()
    {
        SpacelaneStartReason actual = EnumConverter.ConvertToEnumOrDefault(
            "not a real reason",
            SpacelaneStartReason.Discovered
        );
        Assert.Equal(SpacelaneStartReason.Discovered, actual);
    }

    [Fact]
    public void ConvertToEnumOrNull_ValidInput_Enum()
    {
        SpacelaneStartReason? actual = EnumConverter.ConvertToEnumOrNull<SpacelaneStartReason>(
            "Created"
        );
        Assert.Equal(SpacelaneStartReason.Created, actual);
    }

    [Fact]
    public void ConvertToEnumOrNull_InvalidInput_Null()
    {
        SpacelaneStartReason? actual = EnumConverter.ConvertToEnumOrNull<SpacelaneStartReason>(
            "not a real reason"
        );
        Assert.Null(actual);
    }
}

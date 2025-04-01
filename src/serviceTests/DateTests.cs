using Date = GalaxyMapSiteApi.Models.Date;

namespace GalaxyMapSiteApi.Tests;

public class DateTests
{
    #region Constants
    private const int MaximumInt = 2147483647;
    private const int MinimumInt = -2147483647;
    #endregion Constants
    #region Constructors
    [Fact]
    public void ParameterlessConstructor_NoInput_IsValid()
    {
        Date expected = new Date(0);
        Assert.Equal(expected, new Date());
    }

    [Fact]
    public void ValueConstructor_Value_IsValid()
    {
        Date expected = new Date();
        expected.Days = MaximumInt;
        Assert.Equal(expected, new Date(MaximumInt));
    }
    #endregion Constructors
    #region Properties
    [Fact]
    public void DateProperty_Set_IsValid()
    {
        Date date = new Date();
        Assert.Equal(0, date.Days);
        date.Days = MaximumInt;
        Assert.Equal(MaximumInt, date.Days);
    }
    #endregion Properties
    #region IEquatable
    [Fact]
    public void IEquatable_SameValue_ReturnTrue()
    {
        Assert.True(new Date(MaximumInt) == new Date(MaximumInt));
    }

    [Fact]
    public void IEquatable_DifferentValue_ReturnFalse()
    {
        Assert.False(new Date(MaximumInt) == new Date(MinimumInt));
    }

    [Fact]
    public void IEquatable_NullValue_ReturnFalse()
    {
        Assert.False(new Date(MaximumInt).Equals(null));
    }

    [Fact]
    public void IEquatable_NonDate_ReturnFalse()
    {
        Assert.False(new Date(MaximumInt).Equals(new System.DateTime()));
    }
    #endregion IEquatable
}

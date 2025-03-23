using DateTime = GalaxyMapSiteApi.Models.DateTime;

namespace GalaxyMapSiteApi.Tests;

public class DateTimeTests
{
    #region Constants
    private const int MaximumInt = 2147483647;
    private const int MinimumInt = -2147483647;
    #endregion Constants
    #region Constructors
    [Fact]
    public void ParameterlessConstructor_NoInput_IsValid()
    {
        DateTime expected = new DateTime(0);
        Assert.Equal(expected, new DateTime());
    }
    [Fact]
    public void LongConstructor_Value_IsValid()
    {
        DateTime expected = new DateTime();
        expected.Minutes = long.MaxValue;
        Assert.Equal(expected, new DateTime(long.MaxValue));
    }
    [Fact]
    public void IntConstructor_Value_IsValid()
    {
        DateTime expected = new DateTime();
        expected.Minutes = MaximumInt;
        Assert.Equal(expected, new DateTime(MaximumInt));
    }
    #endregion Constructors
    #region Properties
    [Fact]
    public void DateTimeProperty_Set_IsValid()
    {
        DateTime DateTime = new DateTime();
        Assert.Equal(0, DateTime.Minutes);
        DateTime.Minutes = MaximumInt;
        Assert.Equal(MaximumInt, DateTime.Minutes);
    }
    #endregion Properties
    #region IEquatable
    [Fact]
    public void IEquatable_SameValue_ReturnTrue()
    {
        Assert.True(new DateTime(MaximumInt) == new DateTime(MaximumInt));
    }
    [Fact]
    public void IEquatable_DifferentValue_ReturnFalse()
    {
        Assert.False(new DateTime(MaximumInt) == new DateTime(MinimumInt));
    }
    [Fact]
    public void IEquatable_NullValue_ReturnFalse()
    {
        Assert.False(new DateTime(MaximumInt).Equals(null));
    }
    [Fact]
    public void IEquatable_NonDateTime_ReturnFalse()
    {
        Assert.False(new DateTime(MaximumInt).Equals(new System.DateTime()));
    }
    #endregion IEquatable
}

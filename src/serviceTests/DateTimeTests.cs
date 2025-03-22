using DateTime = GalaxyMapSiteApi.Models.DateTime;

namespace GalaxyMapSiteApi.Tests;

public class DateTimeTests
{
    #region Constants
    private const string MissingColonString = "123123";
    private const string MaximumString = "2147483647:2147483647";
    private const string TooLargeString = "2147483648:2147483648";
    private const string MinimumString = "-2147483647:-2147483647";
    private const string TooSmallString = "-2147483649:-2147483649";
    private const int MaximumInt = 2147483647;
    private const int MinimumInt = -2147483647;
    #endregion Constants
    #region Constructors
    [Fact]
    public void StringConstructor_InputNoColon_ThrowsException()
    {
        Assert.Throws<Exception>(() => new DateTime(MissingColonString));
    }
    [Fact]
    public void StringConstructor_InputMaximum_IsValid()
    {
        DateTime dateTime = new DateTime(MaximumString);
        Assert.Equal(MaximumInt, dateTime.Date);
        Assert.Equal(MaximumInt, dateTime.Time);
    }
    [Fact]
    public void StringConstructor_InputMinimum_IsValid()
    {
        DateTime dateTime = new DateTime(MinimumString);
        Assert.Equal(MinimumInt, dateTime.Date);
        Assert.Equal(MinimumInt, dateTime.Time);
    }
    [Fact]
    public void StringConstructor_InputTooLarge_ThrowsException()
    {
        Assert.Throws<Exception>(() => new DateTime(TooLargeString));
    }
    [Fact]
    public void StringConstructor_InputTooSmall_ThrowsException()
    {
        Assert.Throws<Exception>(() => new DateTime(TooSmallString));
    }
    [Fact]
    public void ParameterlessConstructor_NoInput_IsValid()
    {
        DateTime expected = new DateTime(0, 0);
        Assert.Equal(expected, new DateTime());
    }
    #endregion Constructors
    #region Properties
    [Fact]
    public void DateProperty_Set_IsValid()
    {
        DateTime dateTime = new DateTime();
        Assert.Equal(0, dateTime.Date);
        dateTime.Date = MaximumInt;
        Assert.Equal(MaximumInt, dateTime.Date);
    }
    [Fact]
    public void TimeProperty_Set_IsValid()
    {
        DateTime dateTime = new DateTime();
        Assert.Equal(0, dateTime.Time);
        dateTime.Time = MaximumInt;
        Assert.Equal(MaximumInt, dateTime.Time);
    }
    #endregion Properties
    #region IsValidEncodedValue
    [Fact]
    public void IsValidEncodedValue_InputNoColon_ReturnFalse()
    {
        Assert.False(DateTime.IsValidEncodedValue(MissingColonString));
    }
    [Fact]
    public void IsValidEncodedValue_MaximumValues_ReturnTrue()
    {
        Assert.True(DateTime.IsValidEncodedValue(MaximumString));
    }
    [Fact]
    public void IsValidEncodedValue_MinimumValues_ReturnTrue()
    {
        Assert.True(DateTime.IsValidEncodedValue(MinimumString));
    }
    #endregion IsValidEncodedValue
    #region IEquatable
    [Fact]
    public void IEquatable_SameValue_ReturnTrue()
    {
        Assert.True(new DateTime(MaximumString) == new DateTime(MaximumString));
    }
    [Fact]
    public void IEquatable_DifferentValue_ReturnFalse()
    {
        Assert.False(new DateTime(MaximumString) == new DateTime(MinimumString));
    }
    [Fact]
    public void IEquatable_NullValue_ReturnFalse()
    {
        Assert.False(new DateTime(MaximumString).Equals(null));
    }
    [Fact]
    public void IEquatable_NonDateTime_ReturnFalse()
    {
        Assert.False(new DateTime(MaximumString).Equals(new System.DateTime()));
    }
    #endregion IEquatable
}

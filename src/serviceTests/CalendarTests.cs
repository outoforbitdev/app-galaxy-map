using GalaxyMapSiteApi.Models;
using DateTime = GalaxyMapSiteApi.Models.DateTime;

namespace GalaxyMapSiteApi.Tests;

public class CalendarTests
{
    #region Constants
    private const int Epoch = 100;
    private const int DaysPerYear = 368;
    private const int HoursPerDay = 24;
    private const int MinutesPerHour = 60;
    private static Instance testInstance { get; } = new Instance("id", "name");
    private static Calendar testCalendar { get; } = new Calendar(testInstance, "id", "Test", Epoch, MinutesPerHour, HoursPerDay, DaysPerYear);
    private static Date MaxDate { get; }  = new Date(Date.Max);
    private static Date AfterEpochDate { get; } = new Date(Epoch + DaysPerYear + 50);
    private static Date ZeroYearDate { get; } = new Date(Epoch + 50);
    private static Date BeforeEpochDate { get; } = new Date(-100);
    private static Date MinDate { get; } = new Date(Date.Min);
    private static DateTime MaxDateTime { get; } = new DateTime(DateTime.Max);
    private static DateTime AfterEpochDateTime { get; } = new DateTime((Epoch + DaysPerYear + 50) * HoursPerDay * MinutesPerHour + 365);
    private static DateTime ZeroYearDateTime { get; } = new DateTime((Epoch) * HoursPerDay * MinutesPerHour + 365);
    private static DateTime BeforeEpochDateTime { get; } = new DateTime(-100 * HoursPerDay * MinutesPerHour + 365);
    private static DateTime MinDateTime { get; } = new DateTime(DateTime.Min);
    #endregion Constants
    #region Conversions
    [Fact]
    public void GetDate_ValidInput_IsValid()
    {
        Date date = testCalendar.GetDate(AfterEpochDateTime);
        Assert.Equal(AfterEpochDate, date);
    }
    [Fact]
    public void GetDate_InvalidInput_ThrowsException()
    {
        Assert.Throws<Exception>(() => testCalendar.GetDate(MaxDateTime));
    }
    [Fact]
    public void TryGetDate_ValidInput_ReturnsTrue()
    {
        Date date;
        Assert.True(testCalendar.TryGetDate(AfterEpochDateTime, out date));
        Assert.Equal(AfterEpochDate, date);
    }
    [Fact]
    public void TryGetDate_InvalidInput_ReturnsFalse()
    {
        DateTime testDateTime = new DateTime(DateTime.Max);
        Date date;
        Assert.False(testCalendar.TryGetDate(testDateTime, out date));
    }
    [Fact]
    public void GetDateTime_ValidInput_IsValid()
    {
        DateTime dateTime = testCalendar.GetDateTime(AfterEpochDate);
        DateTime expected = new DateTime(AfterEpochDate.Days * HoursPerDay * MinutesPerHour);
        Assert.Equal(expected, dateTime);
    }
    #endregion Conversions
    #region GetValues
    #region GetYear
    [Fact]
    public void GetYear_MaxDate_IsCorrect()
    {
        Assert.Equal(5835553, testCalendar.GetYear(MaxDate));
    }
    [Fact]
    public void GetYear_AfterEpochDate_IsCorrect()
    {
        Assert.Equal(1, testCalendar.GetYear(AfterEpochDate));
    }
    [Fact]
    public void GetYear_ZeroYearDate_IsCorrect()
    {
        Assert.Equal(0, testCalendar.GetYear(ZeroYearDate));
    }
    [Fact]
    public void GetYear_BeforeEpochDate_IsCorrect()
    {
        Assert.Equal(-1, testCalendar.GetYear(BeforeEpochDate));
    }
    [Fact]
    public void GetYear_MinDate_IsCorrect()
    {
        Assert.Equal(-5835554, testCalendar.GetYear(MinDate));
    }
    [Fact]
    public void GetYear_MaxDateTime_IsCorrect()
    {
        Assert.Equal(17405215951190, testCalendar.GetYear(MaxDateTime));
    }
    [Fact]
    public void GetYear_AfterEpochDateTime_IsCorrect()
    {
        Assert.Equal(1, testCalendar.GetYear(AfterEpochDateTime));
    }
    [Fact]
    public void GetYear_ZeroYearDateTime_IsCorrect()
    {
        Assert.Equal(0, testCalendar.GetYear(ZeroYearDateTime));
    }
    [Fact]
    public void GetYear_BeforeEpochDateTime_IsCorrect()
    {
        Assert.Equal(-1, testCalendar.GetYear(BeforeEpochDateTime));
    }
    [Fact]
    public void GetYear_MinDateTime_IsCorrect()
    {
        Assert.Equal(-17405215951191, testCalendar.GetYear(MinDateTime));
    }
    #endregion GetYear
    #region GetDay
    [Fact]
    public void GetDay_MaxDate_IsCorrect() {
        Assert.Equal(43, testCalendar.GetDay(MaxDate));
    }
    [Fact]
    public void GetDay_AfterEpochDate_IsCorrect() {
        Assert.Equal(50, testCalendar.GetDay(AfterEpochDate));
    }
    [Fact]
    public void GetDay_ZeroYearDate_IsCorrect() {
        Assert.Equal(50, testCalendar.GetDay(ZeroYearDate));
    }
    [Fact]
    public void GetDay_BeforeEpochDate_IsCorrect() {
        Assert.Equal(168, testCalendar.GetDay(BeforeEpochDate));
    }
    [Fact]
    public void GetDay_MinDate_IsCorrect() {
        Assert.Equal(124, testCalendar.GetDay(MinDate));
    }
    #endregion GetDay
    #endregion GetValues
}

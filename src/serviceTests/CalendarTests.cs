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
    private static Calendar testCalendar { get; } = new Calendar(testInstance, "id", "Test", Epoch, MinutesPerHour, HoursPerDay, DaysPerYear, true, "BE", "AE");
    private static Calendar testCalendarNoZeroYear { get;} = new Calendar(testInstance, "id", "Test", Epoch, MinutesPerHour, HoursPerDay, DaysPerYear, false, "BE", "AE");
    private static Date MaxDate { get; }  = new Date(Date.Max);
    private static Date AfterEpochDate { get; } = new Date(Epoch + DaysPerYear + 50);
    private static Date ZeroYearDate { get; } = new Date(Epoch + 50);
    private static Date BeforeEpochDate { get; } = new Date(-100);
    private static Date MinDate { get; } = new Date(Date.Min);
    private static DateTime MaxDateTime { get; } = new DateTime(DateTime.Max);
    private static DateTime AfterEpochDateTime { get; } = new DateTime((Epoch + DaysPerYear + 50) * HoursPerDay * MinutesPerHour + 245);
    private static DateTime ZeroYearDateTime { get; } = new DateTime((Epoch + 50) * HoursPerDay * MinutesPerHour + 245);
    private static DateTime BeforeEpochDateTime { get; } = new DateTime(-100 * HoursPerDay * MinutesPerHour + 245);
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
    public static IEnumerable<object[]> GetYearDateData =>
        new List<object[]>
        {
            new object[]{5835553, testCalendar, MaxDate},
            new object[]{1, testCalendar, AfterEpochDate},
            new object[]{0, testCalendar, ZeroYearDate},
            new object[]{-1, testCalendar, BeforeEpochDate},
            new object[]{-5835554, testCalendar, MinDate},
            new object[]{2, testCalendarNoZeroYear, AfterEpochDate},
            new object[]{1, testCalendarNoZeroYear, ZeroYearDate},
            new object[]{-1, testCalendarNoZeroYear, BeforeEpochDate},
        };
    [Theory]
    [MemberData(nameof(GetYearDateData))]
    public void GetYear_Date_IsCorrect(int expected, Calendar calendar, Date date) {
        Assert.Equal(expected, calendar.GetYear(date));
    }
    public static IEnumerable<object[]> GetYearDateTimeData =>
        new List<object[]>
        {
            new object[]{16983695651, testCalendar, MaxDateTime},
            new object[]{1, testCalendar, AfterEpochDateTime},
            new object[]{0, testCalendar, ZeroYearDateTime},
            new object[]{-1, testCalendar, BeforeEpochDateTime},
            new object[]{-16983695653, testCalendar, MinDateTime},
            new object[]{2, testCalendarNoZeroYear, AfterEpochDateTime},
            new object[]{1, testCalendarNoZeroYear, ZeroYearDateTime},
            new object[]{-1, testCalendarNoZeroYear, BeforeEpochDateTime},
        };
    [Theory]
    [MemberData(nameof(GetYearDateTimeData))]
    public void GetYear_DateTime_IsCorrect(long expected, Calendar calendar, DateTime date) {
        Assert.Equal(expected, calendar.GetYear(date));
    }
    #endregion GetYear
    #region GetDay
    public static IEnumerable<object[]> GetDayDateData =>
        new List<object[]>
        {
            new object[]{43, testCalendar, MaxDate},
            new object[]{50, testCalendar, AfterEpochDate},
            new object[]{50, testCalendar, ZeroYearDate},
            new object[]{168, testCalendar, BeforeEpochDate},
            new object[]{124, testCalendar, MinDate},
            new object[]{50, testCalendarNoZeroYear, AfterEpochDate},
            new object[]{50, testCalendarNoZeroYear, ZeroYearDate},
            new object[]{168, testCalendarNoZeroYear, BeforeEpochDate},
        };
    [Theory]
    [MemberData(nameof(GetDayDateData))]
    public void GetDay_Date_IsCorrect(int expected, Calendar calendar, Date date) {
        Assert.Equal(expected, calendar.GetDay(date));
    }
    public static IEnumerable<object[]> GetDayDateTimeData =>
        new List<object[]>
        {
            new object[]{332, testCalendar, MaxDateTime},
            new object[]{50, testCalendar, AfterEpochDateTime},
            new object[]{50, testCalendar, ZeroYearDateTime},
            new object[]{168, testCalendar, BeforeEpochDateTime},
            new object[]{204, testCalendar, MinDateTime},
            new object[]{50, testCalendarNoZeroYear, AfterEpochDateTime},
            new object[]{50, testCalendarNoZeroYear, ZeroYearDateTime},
            new object[]{168, testCalendarNoZeroYear, BeforeEpochDateTime},
        };
    [Theory]
    [MemberData(nameof(GetDayDateTimeData))]
    public void GetDay_DateTime_IsCorrect(int expected, Calendar calendar, DateTime dateTime) {
        Assert.Equal(expected, calendar.GetDay(dateTime));
    }
    #endregion GetDay
    #region GetHour
    public static IEnumerable<object[]> GetHourDateTimeData =>
        new List<object[]>
        {
            new object[]{0, testCalendar, MaxDateTime},
            new object[]{4, testCalendar, AfterEpochDateTime},
            new object[]{4, testCalendar, ZeroYearDateTime},
            new object[]{4, testCalendar, BeforeEpochDateTime},
            new object[]{0, testCalendar, MinDateTime},
            new object[]{4, testCalendarNoZeroYear, AfterEpochDateTime},
            new object[]{4, testCalendarNoZeroYear, ZeroYearDateTime},
            new object[]{4, testCalendarNoZeroYear, BeforeEpochDateTime},
        };
    [Theory]
    [MemberData(nameof(GetHourDateTimeData))]
    public void GetHour_DateTime_IsCorrect(int expected, Calendar calendar, DateTime dateTime) {
        Assert.Equal(expected, calendar.GetHour(dateTime));
    }
    #endregion GetHour
    #region GetMinute
    public static IEnumerable<object[]> GetMinuteDateTimeData =>
        new List<object[]>
        {
            new object[]{0, testCalendar, MaxDateTime},
            new object[]{5, testCalendar, AfterEpochDateTime},
            new object[]{5, testCalendar, ZeroYearDateTime},
            new object[]{5, testCalendar, BeforeEpochDateTime},
            new object[]{0, testCalendar, MinDateTime},
            new object[]{5, testCalendarNoZeroYear, AfterEpochDateTime},
            new object[]{5, testCalendarNoZeroYear, ZeroYearDateTime},
            new object[]{5, testCalendarNoZeroYear, BeforeEpochDateTime},
        };
    [Theory]
    [MemberData(nameof(GetMinuteDateTimeData))]
    public void GetMinute_DateTime_IsCorrect(int expected, Calendar calendar, DateTime dateTime) {
        Assert.Equal(expected, calendar.GetMinute(dateTime));
    }
    #endregion GetMinute
    #region GetDateString
    #endregion GetDateString
    #endregion GetValues
}

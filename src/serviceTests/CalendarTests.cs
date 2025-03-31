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
    private static Calendar testCalendarNoZeroYear { get;} = new Calendar(testInstance, "id", "Test", Epoch, MinutesPerHour, HoursPerDay, DaysPerYear, false);
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
    public static IEnumerable<object[]> GetDateValidData =>
        new List<object[]>
        {
            new object[]{testCalendar, AfterEpochDateTime, AfterEpochDate},
            new object[]{testCalendar, ZeroYearDateTime, ZeroYearDate},
            new object[]{testCalendar, BeforeEpochDateTime, BeforeEpochDate},
            new object[]{testCalendar, new DateTime(((long)Date.Max + 1) * 24 * 60 - 1), MaxDate},
            new object[]{testCalendar, new DateTime(((long)Date.Min) * 24 * 60), MinDate},
        };
    [Theory]
    [MemberData(nameof(GetDateValidData))]
    public void GetDate_ValidInput_IsValid(Calendar calendar, DateTime dateTime, Date expected)
    {
        Date date = calendar.GetDate(dateTime);
        Assert.Equal(expected, date);
        Assert.Equal(testCalendar.GetYear(date), testCalendar.GetYear(dateTime));
        Assert.Equal(testCalendar.GetDay(date), testCalendar.GetDay(dateTime, true));
    }
    public static IEnumerable<object[]> GetDateExceptionData =>
        new List<object[]>
        {
            new object[]{testCalendar, MaxDateTime},
            new object[]{testCalendar, MinDateTime},
            new object[]{testCalendar, new DateTime(((long)Date.Max + 1) * 24 * 60)},
            new object[]{testCalendar, new DateTime(((long)Date.Min - 1) * 24 * 60)},
        };
    [Theory]
    [MemberData(nameof(GetDateExceptionData))]
    public void GetDate_InvalidInput_ThrowsException(Calendar calendar, DateTime dateTime)
    {
        Assert.Throws<Exception>(() => calendar.GetDate(dateTime));
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
            new object[]{-1, testCalendar, new Date(-267)},
            new object[]{-1, testCalendar, new Date(-268)},
            new object[]{-2, testCalendar, new Date(-269)},
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
    #endregion GetValues
    #region GetDateString
    public static IEnumerable<object[]> GetDateStringDateData =>
        new List<object[]>
        {
            new object[]{"1 AE", testCalendar, AfterEpochDate, DateFormat.YearOnly},
            new object[]{"0 AE", testCalendar, ZeroYearDate, DateFormat.YearOnly},
            new object[]{"1 BE", testCalendar, BeforeEpochDate, DateFormat.YearOnly},
            new object[]{"2", testCalendarNoZeroYear, AfterEpochDate, DateFormat.YearOnly},
            new object[]{"1", testCalendarNoZeroYear, ZeroYearDate, DateFormat.YearOnly},
            new object[]{"-1", testCalendarNoZeroYear, BeforeEpochDate, DateFormat.YearOnly},
            new object[]{"1:50 AE", testCalendar, AfterEpochDate, DateFormat.YearColonDay},
            new object[]{"0:50 AE", testCalendar, ZeroYearDate, DateFormat.YearColonDay},
            new object[]{"1:168 BE", testCalendar, BeforeEpochDate, DateFormat.YearColonDay},
            new object[]{"2:50", testCalendarNoZeroYear, AfterEpochDate, DateFormat.YearColonDay},
            new object[]{"1:50", testCalendarNoZeroYear, ZeroYearDate, DateFormat.YearColonDay},
            new object[]{"-1:168", testCalendarNoZeroYear, BeforeEpochDate, DateFormat.YearColonDay},
        };
    [Theory]
    [MemberData(nameof(GetDateStringDateData))]
    public void GetDateString_Date_IsCorrect(string expected, Calendar calendar, Date date, DateFormat format) {
        Assert.Equal(expected, calendar.GetDateString(date, format));
    }
    public static IEnumerable<object[]> GetDateStringDateTimeData =>
        new List<object[]>
        {
            new object[]{"1 AE", testCalendar, AfterEpochDateTime, DateFormat.YearOnly},
            new object[]{"0 AE", testCalendar, ZeroYearDateTime, DateFormat.YearOnly},
            new object[]{"1 BE", testCalendar, BeforeEpochDateTime, DateFormat.YearOnly},
            new object[]{"2", testCalendarNoZeroYear, AfterEpochDateTime, DateFormat.YearOnly},
            new object[]{"1", testCalendarNoZeroYear, ZeroYearDateTime, DateFormat.YearOnly},
            new object[]{"-1", testCalendarNoZeroYear, BeforeEpochDateTime, DateFormat.YearOnly},
            new object[]{"1:50 AE", testCalendar, AfterEpochDateTime, DateFormat.YearColonDay},
            new object[]{"0:50 AE", testCalendar, ZeroYearDateTime, DateFormat.YearColonDay},
            new object[]{"1:168 BE", testCalendar, BeforeEpochDateTime, DateFormat.YearColonDay},
            new object[]{"2:50", testCalendarNoZeroYear, AfterEpochDateTime, DateFormat.YearColonDay},
            new object[]{"1:50", testCalendarNoZeroYear, ZeroYearDateTime, DateFormat.YearColonDay},
            new object[]{"-1:168", testCalendarNoZeroYear, BeforeEpochDateTime, DateFormat.YearColonDay},
        };
    [Theory]
    [MemberData(nameof(GetDateStringDateTimeData))]
    public void GetDateString_DateTime_IsCorrect(string expected, Calendar calendar, DateTime dateTime, DateFormat format) {
        Assert.Equal(expected, calendar.GetDateString(dateTime, format));
    }
    #endregion GetDateString
    #region GetTimeString
    public static IEnumerable<object[]> GetTimeStringDateTimeData =>
        new List<object[]>
        {
            new object[]{"04:05", testCalendar, AfterEpochDateTime},
            new object[]{"04:05", testCalendar, ZeroYearDateTime},
            new object[]{"04:05", testCalendar, BeforeEpochDateTime},
        };
    [Theory]
    [MemberData(nameof(GetTimeStringDateTimeData))]
    public void GetTimeString_DateTime_IsCorrect(string expected, Calendar calendar, DateTime dateTime) {
        Assert.Equal(expected, calendar.GetTimeString(dateTime));
    }
    #endregion GetTimeString
}

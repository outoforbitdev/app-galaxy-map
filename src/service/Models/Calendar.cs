using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GalaxyMapSiteApi.Models;

public enum DateFormat
{
    YearOnly,
    YearColonDay,
}

[Table("calendars")]
public class Calendar : InstanceEntity
{
    #region Properties
    public string Name { get; set; }
    public required int Epoch { get; set; }
    private long EpochMinutes
    {
        get { return Epoch * MinutesPerDay; }
    }
    public required int MinutesPerHour { get; set; }
    public required int HoursPerDay { get; set; }
    private int MinutesPerDay
    {
        get { return MinutesPerHour * HoursPerDay; }
    }
    public required int DaysPerYear { get; set; }
    public required bool ZeroYearAfterEpoch { get; set; }
    public string? AfterEpochSuffix { get; set; }
    public string? BeforeEpochSuffix { get; set; }
    #endregion Properties
    #region Constructors
    [SetsRequiredMembers]
    public Calendar()
        : this(null!, string.Empty, string.Empty, 0, 0, 0, 0, false) { }

    [SetsRequiredMembers]
    public Calendar(
        Instance instance,
        string id,
        string name,
        int epoch,
        int minutesPerHour,
        int hoursPerDay,
        int daysPerYear,
        bool zeroYearAfterEpoch,
        string? beforeEpochSuffix = null,
        string? afterEpochSuffix = null
    )
    {
        Instance = instance;
        InstanceId = instance.Id;
        Id = id;
        Name = name;
        Epoch = epoch;
        MinutesPerHour = minutesPerHour;
        HoursPerDay = hoursPerDay;
        DaysPerYear = daysPerYear;
        ZeroYearAfterEpoch = zeroYearAfterEpoch;
        BeforeEpochSuffix = beforeEpochSuffix;
        AfterEpochSuffix = afterEpochSuffix;
    }
    #endregion Constructors
    #region Methods
    #region Conversions
    /// <summary>
    /// Convert a DateTime to a Date
    /// </summary>
    /// <param name="dateTime">The DateTime to convert</param>
    /// <returns>Date equivalent of the provided DateTime</returns>
    /// <exception cref="Exception">Thrown if the DateTime is too large to be converted to a Date</exception>
    public Date GetDate(DateTime dateTime)
    {
        if (!TryGetDate(dateTime, out Date date))
        {
            throw new Exception(
                $"DateTime {dateTime.Minutes} is equivalent to Date {dateTime.Minutes / MinutesPerDay} which is a greater magnitude than allowed for a date value"
            );
        }
        return date;
    }

    /// <summary>
    /// Attempt to convert a DateTime to a Date
    /// </summary>
    /// <param name="dateTime">The DateTime to convert</param>
    /// <param name="date">The converted Date (if successful)</param>
    /// <returns>True if the DateTime was converted, false otherwise</returns>
    public bool TryGetDate(DateTime dateTime, out Date date)
    {
        long days = dateTime.Minutes / MinutesPerDay;
        if (dateTime.Minutes < EpochMinutes && dateTime.Minutes % MinutesPerDay != 0)
        {
            days--;
        }
        if (days > (long)Date.Max || days < (long)Date.Min)
        {
            date = new Date();
            return false;
        }
        date = new Date((int)days);
        return true;
    }

    /// <summary>
    /// Convert a Date to a DateTime
    /// </summary>
    /// <param name="date">The Date to convert</param>
    /// <returns>The DateTime equivalent to the provided Date</returns>
    public DateTime GetDateTime(Date date)
    {
        return new DateTime((long)date.Days * MinutesPerDay);
    }
    #endregion Conversions
    #region GetValues
    /// <summary>
    /// Get the year represented by the provided Date
    /// </summary>
    /// <param name="date">Date value to retrieve the year from</param>
    /// <returns>Year before or after epoch</returns>
    public int GetYear(Date date)
    {
        int year = GetYearWithZeroYear(date);
        if (year >= 0 && !ZeroYearAfterEpoch)
        {
            year++;
        }
        return year;
    }

    /// <summary>
    /// Get the year represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">DateTime value to retrieve the year from</param>
    /// <returns>Year before or after epoch</returns>
    public long GetYear(DateTime dateTime)
    {
        long year = GetYearWithZeroYear(dateTime);
        if (year >= 0 && !ZeroYearAfterEpoch)
        {
            year++;
        }
        return year;
    }

    /// <summary>
    /// Get the year represented by the provided Date, including a Zero year
    /// </summary>
    /// <param name="date">Date value to retrieve the year from</param>
    /// <returns>Year before or after epoch</returns>
    private int GetYearWithZeroYear(Date date)
    {
        long days = (long)date.Days - Epoch;
        int year = (int)(days / DaysPerYear);
        // If the days value is before the epoch, and not the first day of the year, it should be rounded down to the start of the year
        if (days < 0 && days % DaysPerYear != 0)
        {
            year = year - 1;
        }
        return year;
    }

    /// <summary>
    /// Get the year represented by the provided DateTime, including a Zero year
    /// </summary>
    /// <param name="dateTime">DateTime value to retrieve the year from</param>
    /// <returns>Year before or after epoch</returns>
    private long GetYearWithZeroYear(DateTime dateTime)
    {
        long days = dateTime.Minutes / MinutesPerDay - Epoch;
        long year = days / DaysPerYear;
        // If the days value is before the epoch, and not the first day of the year, it should be rounded down to the start of the year
        if (
            dateTime.Minutes < EpochMinutes
            && dateTime.Minutes % (DaysPerYear * MinutesPerDay) != 0
        )
        {
            year = year - 1;
        }
        return year;
    }

    /// <summary>
    /// Get the Date value of the first day of the year represented by the provided Date
    /// </summary>
    /// <param name="date">The Date value to interpret</param>
    /// <returns>An integer representing the Date value of the first day of the year</returns>
    private int GetYearInDays(Date date)
    {
        return GetYearWithZeroYear(date) * DaysPerYear + Epoch;
    }

    /// <summary>
    /// Get the Date value of the first day of the year represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">The DateTime value to interpret</param>
    /// <returns>An integer representing the DateTime value of the first day of the year</returns>
    private long GetYearInDays(DateTime dateTime)
    {
        return GetYearWithZeroYear(dateTime) * DaysPerYear + Epoch;
    }

    /// <summary>
    /// Get the day of year represented by the provided Date
    /// </summary>
    /// <param name="date">Date value to retrieve the year from</param>
    /// <returns>Day of year</returns>
    public int GetDay(Date date)
    {
        return date.Days - GetYearInDays(date);
    }

    /// <summary>
    /// Get the day of year represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">DateTime value to retrieve the year from</param>
    /// <returns>Day of year</returns>
    public long GetDay(DateTime dateTime)
    {
        return GetDayInMinutes(dateTime) / MinutesPerDay - GetYearInDays(dateTime);
    }

    /// <summary>
    /// Get the DateTime value of the start of the day represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">The DateTime to convert</param>
    /// <returns>A long representing the DateTime of the start of the day</returns>
    private long GetDayInMinutes(DateTime dateTime)
    {
        long day = dateTime.Minutes / MinutesPerDay;
        if (dateTime.Minutes < EpochMinutes && dateTime.Minutes % MinutesPerDay != 0)
        {
            day--;
        }
        return day * MinutesPerDay;
    }

    public int GetMonth(Date date)
    {
        throw new NotImplementedException();
    }

    public int GetMonth(DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get the hour of the day represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">The DateTime value to interpret</param>
    /// <returns>The hour of the day</returns>
    public int GetHour(DateTime dateTime)
    {
        return (int)(dateTime.Minutes - GetDayInMinutes(dateTime)) / MinutesPerHour;
    }

    /// <summary>
    /// Get the minute of the hour represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">The DateTime value to interpret</param>
    /// <returns>The minute of the hour</returns>
    public int GetMinute(DateTime dateTime)
    {
        return (int)(dateTime.Minutes - GetDayInMinutes(dateTime))
            - GetHour(dateTime) * MinutesPerHour;
    }
    #endregion GetValues
    #region GetStrings
    /// <summary>
    /// Get a string intepretation of the provided Date
    /// </summary>
    /// <param name="date">The Date to interpret</param>
    /// <param name="format">The DateFormat to use for the string</param>
    /// <returns>A string representing the provided Date</returns>
    public string GetDateString(Date date, DateFormat format)
    {
        return GetDateString(GetYear(date), GetDay(date), format);
    }

    /// <summary>
    /// Get a string intepretation of the date represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">The DateTime to interpret</param>
    /// <param name="format">The DateFormat to use for the string</param>
    /// <returns>A string representing the date of the provided DateTime</returns>
    public string GetDateString(DateTime dateTime, DateFormat format)
    {
        return GetDateString(GetYear(dateTime), GetDay(dateTime), format);
    }

    /// <summary>
    /// Get a string intepretation of the time represented by the provided DateTime
    /// </summary>
    /// <param name="dateTime">The DateTime to interpret</param>
    /// <returns>A string representing the time of the provided DateTime</returns>
    public string GetTimeString(DateTime dateTime)
    {
        return GetTime(GetHour(dateTime), GetMinute(dateTime));
    }

    /// <summary>
    /// Get a string representing the provided year and day
    /// </summary>
    /// <param name="year">Year to use in the string</param>
    /// <param name="day">Day of year to use in the string</param>
    /// <param name="format">DateFormat to use for the string</param>
    /// <returns>A string representing the provided year and day</returns>
    private string GetDateString(long year, long day, DateFormat format)
    {
        switch (format)
        {
            case DateFormat.YearOnly:
                return GetYearString(year);
            case DateFormat.YearColonDay:
                return GetYearColonDayString(year, day);
            default:
                return GetYearColonDayString(year, day);
        }
    }

    /// <summary>
    /// Add the appropriate suffix to the provided string, based on the provided year
    /// </summary>
    /// <param name="year">The year to use for determining the suffix</param>
    /// <param name="dateString">The string to append the suffix</param>
    /// <returns>The provided string with the appropriate suffix</returns>
    private string AddSuffix(long year, string dateString)
    {
        if (year >= 0 && AfterEpochSuffix is not null)
        {
            return $"{dateString} {AfterEpochSuffix}";
        }
        else if (year < 0 && BeforeEpochSuffix is not null)
        {
            return $"{dateString} {BeforeEpochSuffix}";
        }
        return dateString;
    }

    /// <summary>
    /// Get the string representing the provided year in the format "{year} {suffix}"
    /// </summary>
    /// <param name="year">The year to derive the string from</param>
    /// <returns>String representing the provided year</returns>
    private string GetYearString(long year)
    {
        if (year < 0 && BeforeEpochSuffix is not null)
        {
            return AddSuffix(year, $"{year *= -1}");
        }
        return AddSuffix(year, year.ToString());
    }

    /// <summary>
    /// Get the string representing the provided year in the format "{year}:{day} {suffix}
    /// </summary>
    /// <param name="year">The year from which to derive the string</param>
    /// <param name="day">The day from which to derive the string</param>
    /// <returns>String representing the provided year and day</returns>
    private string GetYearColonDayString(long year, long day)
    {
        if (year < 0 && BeforeEpochSuffix is not null)
        {
            return AddSuffix(year, $"{year * -1}:{day}");
        }
        return AddSuffix(year, $"{year}:{day}");
    }

    /// <summary>
    /// Get a string representation of the provided hour and minute in the format "{hour}:{minute}"
    /// </summary>
    /// <param name="hour">The hour from which to derive the string</param>
    /// <param name="minute">The minute from which to derive the string</param>
    /// <returns>String representing of the provided hour and minute</returns>
    private string GetTime(int hour, int minute)
    {
        int hourDigits = 0;
        int minuteDigits = 0;
        int hoursPerDay = HoursPerDay;
        int minutesPerHour = MinutesPerHour;
        while (hoursPerDay != 0)
        {
            hourDigits++;
            hoursPerDay /= 10;
        }
        while (minutesPerHour != 0)
        {
            minuteDigits++;
            minutesPerHour /= 10;
        }
        return $"{hour.ToString($"D{hourDigits}")}:{minute.ToString($"D{minuteDigits}")}";
    }

    /// <summary>
    /// Get a string interpretation of the date and time represented by the provided Date
    /// </summary>
    /// <param name="date">The Date to interpret</param>
    /// <param name="dateFormat">The DateFormat to use for the string</param>
    /// <returns>String representing the provided Date</returns>
    public string GetDateTimeString(Date date, DateFormat dateFormat)
    {
        return GetDateTimeString(GetDateTime(date), dateFormat);
    }

    /// <summary>
    /// Get a string interpretation of the date and time represented by the provided Datetime
    /// </summary>
    /// <param name="dateTime">The DateTime to interpret</param>
    /// <param name="dateFormat">The DateFormat to use for the string</param>
    /// <returns>String representing the provided DateTime</returns>
    public string GetDateTimeString(DateTime dateTime, DateFormat dateFormat)
    {
        return $"{GetDateString(dateTime, dateFormat)} {GetTimeString(dateTime)}";
    }
    #endregion GetStrings
    #endregion Methods
}

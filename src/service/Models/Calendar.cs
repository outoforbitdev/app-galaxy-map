using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace GalaxyMapSiteApi.Models;

public struct Month {
    public string Name;
    public int Length;
}
public enum DateFormat {
    YearOnly,
    YearColonDay,
}

[Table("calendars")]
public class Calendar: InstanceEntity {
    #region Properties
    public string Name { get; set; }
    public required int Epoch { get; set; }
    public required int MinutesPerHour { get; set; }
    public required int HoursPerDay { get; set; }
    private int MinutesPerDay { get { return MinutesPerHour * HoursPerDay; }}
    public required int DaysPerYear { get; set; }
    public required bool ZeroYearAfterEpoch { get; set; }
    public string? AfterEpochSuffix { get; set; }
    public string? BeforeEpochSuffix { get; set; }
    [NotMapped]
    public List<Month> Months { get; set; } = new List<Month>();
    #endregion Properties
    #region Constructors
    [SetsRequiredMembers]
    public Calendar(Instance instance, string id, string name, int epoch, int minutesPerHour, int hoursPerDay, int daysPerYear, bool zeroYearAfterEpoch, string? beforeEpochSuffix = null, string? afterEpochSuffix = null) {
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
    public Date GetDate(DateTime dateTime) {
        long days = dateTime.Minutes / MinutesPerHour / HoursPerDay;
        if (days > (long)Date.Max || days < (long)Date.Min) {
            throw new Exception($"DateTime {dateTime.Minutes} is equivalent to Date {days} is greater than largest possible date value");
        }
        return new Date((int)(dateTime.Minutes / MinutesPerHour / HoursPerDay));
    }
    public bool TryGetDate(DateTime dateTime, out Date date) {
        try {
            date = GetDate(dateTime);
            return true;
        } catch {}
        date = new Date();
        return false;
    }
    public DateTime GetDateTime(Date date) {
        return new DateTime((long)date.Days * HoursPerDay * MinutesPerHour);
    }
    #endregion Conversions
    #region GetValues
    public int GetYear(Date date) {
        int year = GetYearWithZeroYear(date);
        if (year >= 0 && !ZeroYearAfterEpoch) {
            year++;
        }
        return year;
    }
    public long GetYear(DateTime dateTime) {
        long year = GetYearWithZeroYear(dateTime);
        if (year >= 0 && !ZeroYearAfterEpoch) {
            year++;
        }
        return year;
    }
    private int GetYearWithZeroYear(Date date) {
        long days = (long)date.Days - Epoch;
        int year = (int)(days / DaysPerYear);
        if (days < 0 && days % DaysPerYear != 0) {
            year = year - 1;
        }
        return year;
    }
    private long GetYearWithZeroYear(DateTime dateTime) {
        long days = dateTime.Minutes / MinutesPerDay - Epoch;
        long year = days / DaysPerYear;
        if (days < 0 && days % DaysPerYear != 0) {
            year = year - 1;
        }
        return year;
    }
    private int GetYearInDays(Date date) {
        return GetYearWithZeroYear(date) * DaysPerYear + Epoch;
    }
    private long GetYearInDays(DateTime dateTime) {
        return GetYearWithZeroYear(dateTime) * DaysPerYear + Epoch;
    }
    private long GetYearInMinutes(DateTime dateTime) {
        return GetYearWithZeroYear(dateTime) * DaysPerYear * MinutesPerDay;
    }
    public int GetDay(Date date) {
        return date.Days - GetYearInDays(date);
    }
    public long GetDay(DateTime dateTime) {
        return GetDayInMinutes(dateTime) / MinutesPerDay - GetYearInDays(dateTime);
    }
    private long GetDayInMinutes(DateTime dateTime) {
        long day = dateTime.Minutes / MinutesPerDay;
        if (day < 0 && dateTime.Minutes % MinutesPerDay != 0) {
            day--;
        }
        return day * MinutesPerDay;
    }
    public int GetMonth(Date date) {
        throw new NotImplementedException();
    }
    public int GetMonth(DateTime dateTime) {
        throw new NotImplementedException();
    }
    public int GetHour(DateTime dateTime) {
        return (int)(dateTime.Minutes - GetDayInMinutes(dateTime)) / MinutesPerHour;
    }
    public int GetMinute(DateTime dateTime) {
        return (int)(dateTime.Minutes - GetDayInMinutes(dateTime)) - GetHour(dateTime) * MinutesPerHour;
    }
    #endregion GetValues
    #region GetStrings
    public string GetDateString(Date date, DateFormat format) {
        return GetDateString(GetYear(date), GetDay(date), format);
    }
    public string GetDateString(DateTime dateTime, DateFormat format) {
        return GetDateString(GetYear(dateTime), GetDay(dateTime), format);
    }
    public string GetTimeString(DateTime dateTime) {
        return GetTime(GetHour(dateTime), GetMinute(dateTime));
    }
    private string GetDateString(long year, long day, DateFormat format) {
        switch (format) {
            case DateFormat.YearOnly:
                return GetYearString(year);
            case DateFormat.YearColonDay:
                return GetYearColonDayString(year, day);
            default:
                return GetYearColonDayString(year, day);
        }
    }
    private string AddSuffix(long year, string dateString){
        if (year >= 0 && AfterEpochSuffix is not null) {
            return $"{dateString} {AfterEpochSuffix}";
        }
        else if (year < 0 && BeforeEpochSuffix is not null) {
            return $"{dateString} {BeforeEpochSuffix}";
        }
        return dateString;
    }
    private string GetYearString(long year) {
        if (year < 0 && BeforeEpochSuffix is not null){
            return AddSuffix(year, $"{year *= -1}");
        }
        return AddSuffix(year, year.ToString());
    }
    private string GetYearColonDayString(long year, long day) {
        if (year < 0 && BeforeEpochSuffix is not null){
        return AddSuffix(year, $"{year * -1}:{day}");
        }
        return AddSuffix(year, $"{year}:{day}");
    }
    private string GetTime(int hour, int minute) {
        int hourDigits = 0;
        int minuteDigits = 0;
        int hoursPerDay = HoursPerDay;
        int minutesPerHour = MinutesPerHour;
        while (hoursPerDay != 0) {
            hourDigits++;
            hoursPerDay /= 10;
        }
        while (minutesPerHour != 0) {
            minuteDigits++;
            minutesPerHour /= 10;
        }
        return $"{hour.ToString($"D{hourDigits}")}:{minute.ToString($"D{minuteDigits}")}";
    }
    public string GetDateTimeString(Date date, DateFormat dateFormat) {
        return GetDateTimeString(GetDateTime(date), dateFormat);
    }
    public string GetDateTimeString(DateTime dateTime, DateFormat dateFormat) {
        return $"{GetDateString(dateTime, dateFormat)} {GetTimeString(dateTime)}";
    }
    #endregion GetStrings
    #endregion Methods
}
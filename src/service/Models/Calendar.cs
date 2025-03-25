using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GalaxyMapSiteApi.Models;

public struct Month {
    public string Name;
    public int Length;
}

[Table("calendars")]
public class Calendar: InstanceEntity {
    #region Properties
    public string Name { get; set; }
    public int Epoch { get; set; }
    public int MinutesPerHour { get; set; }
    public int HoursPerDay { get; set; }
    public int DaysPerYear { get; set; }
    [NotMapped]
    public List<Month> Months { get; set; } = new List<Month>();
    #endregion Properties
    #region Constructors
    [SetsRequiredMembers]
    public Calendar(Instance instance, string id, string name, int epoch, int minutesPerHour, int hoursPerDay, int daysPerYear) {
        Instance = instance;
        InstanceId = instance.Id;
        Id = id;
        Name = name;
        Epoch = epoch;
        MinutesPerHour = minutesPerHour;
        HoursPerDay = hoursPerDay;
        DaysPerYear = daysPerYear;
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
        long days = (long)date.Days - Epoch;
        int year = (int)(days / DaysPerYear);
        if (days < 0 && days % DaysPerYear != 0) {
            year = year - 1;
        }
        return year;
    }
    public long GetYear(DateTime dateTime) {
        long days = dateTime.Minutes / MinutesPerHour / HoursPerDay - Epoch;
        long year = days / DaysPerYear;
        if (days < 0 && days % DaysPerYear != 0) {
            year = year - 1;
        }
        return year;
    }
    private int GetYearInDays(Date date) {
        return GetYear(date) * DaysPerYear + Epoch;
    }
    private long GetYearInDays(DateTime dateTime) {
        return GetYear(dateTime) * DaysPerYear + Epoch;
    }
    private long GetYearInMinutes(DateTime dateTime) {
        return GetYear(dateTime) * DaysPerYear * HoursPerDay * MinutesPerHour;
    }
    public int GetDay(Date date) {
        return date.Days - GetYearInDays(date);
    }
    public long GetDay(DateTime dateTime) {
        throw new NotImplementedException();
    }
    private long GetDayInMinutes(DateTime dateTime) {
        return dateTime.Minutes / MinutesPerHour / HoursPerDay * HoursPerDay * MinutesPerHour;
    }
    public int GetMonth(Date date) {
        throw new NotImplementedException();
    }
    public int GetMonth(DateTime dateTime) {
        throw new NotImplementedException();
    }
    public int GetHour(DateTime dateTime) {
        return GetMinute(dateTime) / MinutesPerHour;
    }
    public int GetMinute(DateTime dateTime) {
        return (int)(dateTime.Minutes - GetDayInMinutes(dateTime));
    }
    #endregion GetValues
    #endregion Methods
}
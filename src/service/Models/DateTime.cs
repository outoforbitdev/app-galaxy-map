using System.Text.RegularExpressions;

namespace GalaxyMapSiteApi.Models;

public struct DateTime: IEquatable<DateTime> {
    private const string ValidRegex = @"^-?\d+:-?\d+$";
    #region Properties
    public int Date;
    public int Time;
    public string EncodedValue {
        get {
            return $"{Date}:{Time}";
        }
        set {
            if (Regex.IsMatch(value, ValidRegex)) {
                string[] valueList = value.Split(":");
                if (int.TryParse(valueList[0], out Date)) {
                    if (int.TryParse(valueList[1], out Time)) {
                        return;
                    }
                    throw new Exception($"{valueList[1]} is not a valid Time value");
                }
                throw new Exception($"{valueList[0]} is not a valid Date value");
            }
            throw new Exception($"{value} is not a valid DateTime value");
        }
    }
    #endregion Properties
    #region Constructors
    public DateTime(): this(0){}
    public DateTime(string encodedValue) {
        EncodedValue = encodedValue;
    }
    public DateTime(int date): this(date, 0) {}
    public DateTime(int date, int time) {
        Date = date;
        Time = time;
    }
    #endregion Constructors
    #region Static Methods
    public static bool IsValidEncodedValue(string encodedValue) {
        try
        {
            if (Regex.IsMatch(encodedValue, ValidRegex))
            {
                string[] valueList = encodedValue.Split(":");
                if (int.TryParse(valueList[0], out int output))
                {
                    if (int.TryParse(valueList[1], out output))
                    {
                        return true;
                    }
                }
            }
        }
        catch { }
        return false;
    }
    public static DateTime FromString(string value) {
        return new DateTime(value);
    }
    public static bool TryFromString(string value, out DateTime dateTime) {
        try {
            dateTime =  new DateTime(value);
            return true;
        } catch {
            dateTime = new DateTime();
            return false;
        }
    }
    #endregion Static Methods
    #region IEquatable
    public readonly bool Equals(DateTime other)
    {
        return (Date == other.Date) && (Time == other.Time);
    }
    public override readonly bool Equals(object? obj)
    {
        return obj is not null && obj is DateTime time && Equals(time);
    }
    public override int GetHashCode() {
        return EncodedValue.GetHashCode();
    }
    public static bool operator ==(DateTime a, DateTime b) {
        return a.Equals(b);
    }
    public static bool operator !=(DateTime a, DateTime b) {
        return !a.Equals(b);
    }
    #endregion IEquatable
}

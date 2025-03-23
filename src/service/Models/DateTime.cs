namespace GalaxyMapSiteApi.Models;

public struct DateTime: IEquatable<DateTime> {
    #region Properties
    public long Minutes;
    public static long Max { get; } = long.MaxValue;
    public static long Min { get; }  = long.MinValue;
    #endregion Properties
    #region Constructors
    public DateTime(): this(0){}
    public DateTime(long date) {
        Minutes = date;
    }
    public DateTime(int date): this((long)date) {}
    #endregion Constructors
    #region IEquatable
    public readonly bool Equals(DateTime other)
    {
        return Minutes == other.Minutes;
    }
    public override readonly bool Equals(object? obj)
    {
        return obj is not null && obj is DateTime time && Equals(time);
    }
    public override int GetHashCode() {
        return Minutes.GetHashCode();
    }
    public static bool operator ==(DateTime a, DateTime b) {
        return a.Equals(b);
    }
    public static bool operator !=(DateTime a, DateTime b) {
        return !a.Equals(b);
    }
    #endregion IEquatable
}

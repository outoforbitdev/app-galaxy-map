namespace GalaxyMapSiteApi.Models;

public struct Date: IEquatable<Date> {
    #region Properties
    public int Days;
    public static int Max { get; } = int.MaxValue;
    public static int Min { get; }  = int.MinValue;
    #endregion Properties
    #region Constructors
    public Date(): this(0){}
    public Date(int date) {
        Days = date;
    }
    #endregion Constructors
    #region IEquatable
    public readonly bool Equals(Date other)
    {
        return Days == other.Days;
    }
    public override readonly bool Equals(object? obj)
    {
        return obj is not null && obj is Date time && Equals(time);
    }
    public override int GetHashCode() {
        return Days;
    }
    public static bool operator ==(Date a, Date b) {
        return a.Equals(b);
    }
    public static bool operator !=(Date a, Date b) {
        return !a.Equals(b);
    }
    #endregion IEquatable
}

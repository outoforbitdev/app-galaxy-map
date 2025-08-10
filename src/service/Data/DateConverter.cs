using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class DateConverter : ValueConverter<Date, int>
{
    public DateConverter()
        : base(date => date.Days, days => new Date(days)) { }
}

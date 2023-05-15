namespace lab.dotnet6.webapi.Infrastructure.Converters;

public class TimeOnlyTypeConverter : StringTypeConverterBase<TimeOnly>
{
    protected override TimeOnly Parse(string s, IFormatProvider? provider)
    {
        return TimeOnly.Parse(s, provider);
    }

    protected override string ToIsoString(TimeOnly source, IFormatProvider? provider)
    {
        return source.ToString("O", provider);
    }
}
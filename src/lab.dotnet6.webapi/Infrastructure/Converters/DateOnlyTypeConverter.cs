namespace lab.dotnet6.webapi.Infrastructure.Converters;

public class DateOnlyTypeConverter : StringTypeConverterBase<DateOnly>
{
    protected override DateOnly Parse(string s, IFormatProvider? provider)
    {
        return DateOnly.Parse(s, provider);
    }

    protected override string ToIsoString(DateOnly source, IFormatProvider? provider)
    {
        return source.ToString("O", provider);
    }
}
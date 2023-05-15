using System.ComponentModel;
using System.Globalization;

namespace lab.dotnet6.webapi.Infrastructure.Converters;

/// <summary>
/// via, https://stackoverflow.com/questions/69187622/how-can-i-use-dateonly-timeonly-query-parameters-in-asp-net-core-6
/// via, https://github.com/maxkoshevoi/DateOnlyTimeOnly.AspNet/tree/main
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StringTypeConverterBase<T> : System.ComponentModel.TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }

        return base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(string))
        {
            return true;
        }

        return base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string str)
        {
            return this.Parse(str, GetFormat(culture));
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is T typedValue)
        {
            return this.ToIsoString(typedValue, GetFormat(culture));
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    protected abstract T Parse(string s, IFormatProvider? provider);

    protected abstract string ToIsoString(T source, IFormatProvider? provider);

    private static IFormatProvider? GetFormat(CultureInfo? culture)
    {
        DateTimeFormatInfo? formatInfo = null;
        if (culture != null)
        {
            formatInfo = (DateTimeFormatInfo?)culture.GetFormat(typeof(DateTimeFormatInfo));
        }

        return (IFormatProvider?)formatInfo ?? culture;
    }
}
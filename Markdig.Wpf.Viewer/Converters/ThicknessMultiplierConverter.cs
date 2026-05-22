namespace MarkdigWpfViewer.Converters;

using System.Globalization;
using System.Windows;
using System.Windows.Data;

internal class ThicknessMultiplierConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is not Thickness thickness)
            return new Thickness();

        return value switch
        {
            double dValue => Calculate(thickness, dValue),
            float fValue => Calculate(thickness, fValue),
            long lValue => Calculate(thickness, lValue),
            int iValue => Calculate(thickness, iValue),
            byte bValue => Calculate(thickness, bValue),
            string sValue => double.TryParse(sValue, out var result) ? Calculate(thickness, result) : new(),
            _ => new()
        };
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }

    private Thickness Calculate(Thickness main, double mult)
        => new(main.Left * mult, main.Top * mult, main.Right * mult, main.Bottom * mult);
}
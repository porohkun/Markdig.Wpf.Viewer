namespace MarkdigWpfViewer.Styling;

using System.Windows;
using System.Windows.Media;
using Abstractions;

public sealed record MdCodeBlockStyle : IMdStyle
{
    public Brush Background { get; init; } = Brushes.Transparent;
    public Brush BorderBrush { get; init; } = Brushes.Transparent;
    public Thickness BorderThickness { get; init; }
    public Thickness Padding { get; init; }
    public CornerRadius CornerRadius { get; init; }
    public double FontSize { get; init; } = 14;
    public required FontFamily FontFamily { get; init; }
}
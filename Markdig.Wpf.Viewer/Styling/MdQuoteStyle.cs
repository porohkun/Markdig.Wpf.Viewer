namespace MarkdigWpfViewer.Styling;

using System.Windows;
using System.Windows.Media;
using Abstractions;

public sealed record MdQuoteStyle : IMdStyle
{
    public Brush BorderBrush { get; init; } = Brushes.Transparent;
    public Thickness BorderThickness { get; init; }
    public Thickness Padding { get; init; }
    public Thickness Margin { get; init; }
}
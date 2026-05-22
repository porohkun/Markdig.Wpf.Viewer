namespace MarkdigWpfViewer.Styling;

using System.Windows;
using System.Windows.Media;

public sealed record MarkdownInlineCodeStyle
{
    public Brush Background { get; init; } = Brushes.Transparent;
    public Brush Foreground { get; init; } = Brushes.Black;
    public Brush BorderBrush { get; init; } = Brushes.Transparent;
    public Thickness BorderThickness { get; init; }
    public Thickness Padding { get; init; }
    public CornerRadius CornerRadius { get; init; }
    public required FontFamily FontFamily { get; init; }
}
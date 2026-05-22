namespace MarkdigWpfViewer.Styling;

using System.Windows;
using System.Windows.Media;
using Abstractions;

public sealed record MdTableStyle : IMdStyle
{
    public Brush BorderBrush { get; init; } = Brushes.Transparent;
    public Thickness BorderThickness { get; init; }
    public Brush HeaderBackground { get; init; } = Brushes.Transparent;
    public Thickness CellPadding { get; init; }
    public MdParagraphStyle? ParagraphStyle { get; init; }
}
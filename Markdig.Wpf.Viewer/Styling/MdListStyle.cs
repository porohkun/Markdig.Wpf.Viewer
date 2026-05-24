namespace MarkdigWpfViewer.Styling;

using System.Windows;
using Abstractions;

public sealed record MdListStyle : IMdStyle
{
    public Thickness Margin { get; init; }
    public double MarkerWidth { get; init; }
    public Thickness ItemSpacing { get; init; }
}
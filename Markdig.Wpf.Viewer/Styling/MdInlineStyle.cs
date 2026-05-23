namespace MarkdigWpfViewer.Styling;

using System.Windows.Media;
using Abstractions;

public record MdInlineStyle : IMdStyle
{
    public double FontSize { get; init; } = 14;
    public required FontFamily FontFamily { get; init; }
    public required Brush LinkForeground { get; init; }
    public required MdInlineCodeStyle InlineCode { get; init; }
}
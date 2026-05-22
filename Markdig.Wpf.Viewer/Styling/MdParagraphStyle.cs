namespace MarkdigWpfViewer.Styling;

using System.Windows;
using System.Windows.Media;
using Abstractions;

public record MdParagraphStyle : IMdStyle
{
    public Thickness Margin { get; init; }
    public double FontSize { get; init; } = 14;
    public required FontFamily FontFamily { get; init; }
    public required Brush LinkForeground { get; init; }
    public MarkdownInlineCodeStyle? InlineCodeStyle { get; init; }
}
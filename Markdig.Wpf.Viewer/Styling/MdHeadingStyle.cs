namespace MarkdigWpfViewer.Styling;

using System.Windows;

public sealed record MdHeadingStyle : MdParagraphStyle
{
    public FontWeight FontWeight { get; init; }
}
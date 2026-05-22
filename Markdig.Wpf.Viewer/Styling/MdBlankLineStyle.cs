namespace MarkdigWpfViewer.Styling;

using Abstractions;

public sealed record MdBlankLineStyle : IMdStyle
{
    public double Height { get; init; } = 14;
}
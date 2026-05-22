namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record HeadingVm(MdHeadingStyle Style, int Level, IReadOnlyList<InlineVm> Inlines)
    : IMdBlockVm;
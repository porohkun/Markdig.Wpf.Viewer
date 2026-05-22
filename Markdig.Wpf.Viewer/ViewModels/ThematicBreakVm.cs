namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record ThematicBreakVm(MdBlankLineStyle Style)
    : IMdBlockVm;
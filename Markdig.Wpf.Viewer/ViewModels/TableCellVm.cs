namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record TableCellVm(
    MdTableStyle Style,
    IReadOnlyList<InlineVm> Inlines)
    : IMdBlockVm;
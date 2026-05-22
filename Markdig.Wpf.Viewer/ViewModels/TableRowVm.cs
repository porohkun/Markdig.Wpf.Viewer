namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record TableRowVm(MdTableStyle Style, bool IsHeader, IReadOnlyList<TableCellVm> Cells)
    : IMdBlockVm;
namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record TableVm(MdTableStyle Style, int ColumnCount, IReadOnlyList<TableRowVm> Rows)
    : IMdBlockVm;
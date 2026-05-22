namespace MarkdigWpfViewer.ViewModels;

using System.Windows.Controls;
using Abstractions;
using Styling;

public sealed record ListVm(
    MdListStyle Style,
    bool Ordered,
    int Start,
    int Depth,
    IReadOnlyList<ListItemVm> Items,
    DataTemplateSelector MarkdownTemplateSelector)
    : IMdBlockVm;
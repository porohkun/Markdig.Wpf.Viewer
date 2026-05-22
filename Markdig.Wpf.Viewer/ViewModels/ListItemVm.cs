namespace MarkdigWpfViewer.ViewModels;

using System.Windows.Controls;
using Abstractions;
using Styling;

public sealed record ListItemVm(
    MdListStyle Style,
    string MarkerText,
    IReadOnlyList<IMdBlockVm> Blocks,
    DataTemplateSelector MarkdownTemplateSelector)
    : IMdBlockVm;
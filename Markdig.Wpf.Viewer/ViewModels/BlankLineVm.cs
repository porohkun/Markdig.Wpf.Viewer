namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record BlankLineVm(MdBlankLineStyle Style) : IMdBlockVm;
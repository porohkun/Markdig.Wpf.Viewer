namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record CodeBlockVm(MdCodeBlockStyle Style, string? InfoString, string? Language, string Code)
    : IMdBlockVm;
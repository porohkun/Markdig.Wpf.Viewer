namespace MarkdigWpfViewer.ViewModels;

using Abstractions;

public sealed record InlineVm(
    string Text,
    bool Bold = false,
    bool Italic = false,
    bool Strike = false,
    bool Code = false,
    Uri? Hyperlink = null,
    bool LineBreak = false)
    : IMdBlockVm;
namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record ParagraphVm(MdParagraphStyle Style, IReadOnlyList<InlineVm> Inlines)
    : IMdBlockVm;
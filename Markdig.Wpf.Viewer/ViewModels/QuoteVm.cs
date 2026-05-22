namespace MarkdigWpfViewer.ViewModels;

using Abstractions;
using Styling;

public sealed record QuoteVm(MdQuoteStyle Style, IReadOnlyList<IMdBlockVm> Blocks)
    : IMdBlockVm;
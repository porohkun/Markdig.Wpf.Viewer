namespace MarkdigWpfViewer.Abstractions;

using System.Windows;
using Styling;

public interface IMdStyleProvider
{
    MdParagraphStyle ParagraphStyle { get; }

    IReadOnlyList<MdHeadingStyle> HeadingsStyle { get; }

    MarkdownInlineCodeStyle InlineCodeStyle { get; }

    MdCodeBlockStyle CodeBlockStyle { get; }

    MdTableStyle TableStyle { get; }

    MdQuoteStyle QuoteStyle { get; }

    MdListStyle ListStyle { get; }

    MdBlankLineStyle BlankLineStyle { get; }

    DataTemplate ParagraphTemplate { get; }

    DataTemplate HeadingTemplate { get; }

    DataTemplate CodeBlockTemplate { get; }

    DataTemplate QuoteTemplate { get; }

    DataTemplate ListTemplate { get; }

    DataTemplate ListItemTemplate { get; }

    DataTemplate TableTemplate { get; }

    DataTemplate HorizontalRuleTemplate { get; }

    DataTemplate BlankLineTemplate { get; }
}
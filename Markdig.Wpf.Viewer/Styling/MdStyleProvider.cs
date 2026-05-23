namespace MarkdigWpfViewer.Styling;

using System.Windows;
using Abstractions;

internal sealed class MdStyleProvider : IMdStyleProvider
{
    public required MdParagraphStyle ParagraphStyle
    {
        get;
        set => Set(ref field,
            value,
            () =>
            {
                InsertInlineCodeIntoParagraph();
                InsertParagraphIntoList();
                InsertParagraphIntoTable();
            });
    }

    public required IReadOnlyList<MdHeadingStyle> HeadingsStyle
    {
        get;
        set => Set(ref field, value, InsertInlineCodeIntoHeadings);
    }

    public required MdInlineCodeStyle InlineCodeStyle
    {
        get;
        set => Set(ref field,
            value,
            () =>
            {
                InsertInlineCodeIntoParagraph();
                InsertInlineCodeIntoHeadings();
            });
    }

    public required MdCodeBlockStyle CodeBlockStyle { get; set; }

    public required MdTableStyle TableStyle
    {
        get;
        set => Set(ref field, value, InsertParagraphIntoTable);
    }

    public required MdQuoteStyle QuoteStyle { get; set; }

    public required MdListStyle ListStyle
    {
        get;
        set => Set(ref field, value, InsertParagraphIntoList);
    }

    public required MdBlankLineStyle BlankLineStyle { get; set; }

    public required DataTemplate ParagraphTemplate { get; set; }

    public required DataTemplate HeadingTemplate { get; set; }

    public required DataTemplate CodeBlockTemplate { get; set; }

    public required DataTemplate QuoteTemplate { get; set; }

    public required DataTemplate ListTemplate { get; set; }

    public required DataTemplate ListItemTemplate { get; set; }

    public required DataTemplate TableTemplate { get; set; }

    public required DataTemplate HorizontalRuleTemplate { get; set; }

    public required DataTemplate BlankLineTemplate { get; set; }

    private void InsertParagraphIntoTable()
    {
        if (TableStyle != null! && TableStyle.ParagraphStyle != ParagraphStyle)
            TableStyle = TableStyle with { ParagraphStyle = ParagraphStyle };
    }

    private void InsertParagraphIntoList()
    {
        if (ListStyle != null! && ListStyle.ParagraphStyle != ParagraphStyle)
            ListStyle = ListStyle with { ParagraphStyle = ParagraphStyle };
    }

    private void InsertInlineCodeIntoParagraph()
    {
        if (ParagraphStyle != null! && ParagraphStyle.InlineCodeStyle != InlineCodeStyle)
            ParagraphStyle = ParagraphStyle with { InlineCodeStyle = InlineCodeStyle };
    }

    private void InsertInlineCodeIntoHeadings()
    {
        if (HeadingsStyle != null! && HeadingsStyle.Any(h => h.InlineCodeStyle != InlineCodeStyle))
            HeadingsStyle = HeadingsStyle.Select(h => h with { InlineCodeStyle = InlineCodeStyle }).ToArray();
    }

    private void Set<T>(ref T storage, T value, Action? onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return;

        storage = value;
        onChanged?.Invoke();
    }
}

public class MarkdownHeadingStyleArray : List<MdHeadingStyle>
{
}
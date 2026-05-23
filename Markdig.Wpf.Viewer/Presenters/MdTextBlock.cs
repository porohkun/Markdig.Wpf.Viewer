namespace MarkdigWpfViewer.Presenters;

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using Styling;
using ViewModels;

public class MdTextBlock : TextBlock
{
    public static readonly DependencyProperty RunsProperty =
        DependencyProperty.Register(
            nameof(Runs),
            typeof(IEnumerable<InlineVm>),
            typeof(MdTextBlock),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnAnyPropertyChanged));

    public static readonly DependencyProperty LinkForegroundProperty =
        DependencyProperty.Register(
            nameof(LinkForeground),
            typeof(Brush),
            typeof(MdTextBlock),
            new FrameworkPropertyMetadata(Brushes.Blue, FrameworkPropertyMetadataOptions.AffectsMeasure, OnAnyPropertyChanged));

    public static readonly DependencyProperty InlineCodeStyleProperty =
        DependencyProperty.Register(
            nameof(InlineCodeStyle),
            typeof(MdInlineCodeStyle),
            typeof(MdTextBlock),
            new FrameworkPropertyMetadata(
                new MdInlineCodeStyle { FontFamily = new("Consolas") },
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnAnyPropertyChanged));

    public MdTextBlock()
    {
        AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(OnRequestNavigate));
        Rebuild();
    }

    public IEnumerable<InlineVm>? Runs
    {
        get => (IEnumerable<InlineVm>?)GetValue(RunsProperty);
        set => SetValue(RunsProperty, value);
    }

    public Brush LinkForeground
    {
        get => (Brush)GetValue(LinkForegroundProperty);
        set => SetValue(LinkForegroundProperty, value);
    }

    public MdInlineCodeStyle InlineCodeStyle
    {
        get => (MdInlineCodeStyle)GetValue(InlineCodeStyleProperty);
        set => SetValue(InlineCodeStyleProperty, value);
    }

    //private IMdStyleProvider Provider => MarkdownStyling.GetStyleProvider(this)!;

    protected static void OnAnyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((MdTextBlock)d).Rebuild();

    protected virtual void ApplyStyle(Inline inline, MdInlineStyle style, bool hyperlink)
    {
        inline.FontFamily = style.FontFamily;
        inline.FontSize = style.FontSize;

        if (hyperlink)
            inline.Foreground = style.LinkForeground;
    }

    //protected virtual MdInlineStyle GetInlineStyle() => Provider.Inline;

    private static Span Wrap(Inline inner, Action<Span> configure)
    {
        var span = new Span();
        configure(span);
        span.Inlines.Add(inner);
        return span;
    }

    private static void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        try
        {
            if (e.Uri is not null)
            {
                Process.Start(new ProcessStartInfo(e.Uri.ToString()) { UseShellExecute = true });
                e.Handled = true;
            }
        }
        catch
        {
            // deliberately ignored
        }
    }

    //private void ApplyStyle(Inline inline, bool hyperlink = false)
    //    => ApplyStyle(inline, GetInlineStyle(), hyperlink);

    private void Rebuild()
    {
        Inlines.Clear();

        if (Runs is null)
        {
            return;
        }

        foreach (var run in Runs)
        {
            AddRun(run);
        }
    }

    private void AddRun(InlineVm inline)
    {
        if (inline.LineBreak)
        {
            Inlines.Add(new LineBreak());
            return;
        }

        if (inline.Code)
        {
            Inlines.Add(CreateInlineCode(inline.Text));
            return;
        }

        Inline current = new Run(inline.Text ?? string.Empty)
        {
            FontFamily = FontFamily,
            FontSize = FontSize
        };

        if (inline.Strike)
        {
            current = Wrap(current, span => span.TextDecorations = System.Windows.TextDecorations.Strikethrough);
        }

        if (inline.Italic)
        {
            current = Wrap(current, span => span.FontStyle = FontStyles.Italic);
        }

        if (inline.Bold)
        {
            current = Wrap(current, span => span.FontWeight = FontWeights.SemiBold);
        }

        if (inline.Hyperlink is not null)
        {
            var link = new Hyperlink
            {
                NavigateUri = inline.Hyperlink,
                FontFamily = FontFamily,
                FontSize = FontSize,
                Foreground = LinkForeground
            };

            link.Inlines.Add(current);
            current = link;
        }

        Inlines.Add(current);
    }

    private Inline CreateInlineCode(string text)
    {
        var style = InlineCodeStyle;

        var tb = new TextBlock
        {
            Text = text,
            FontSize = FontSize,
            FontFamily = style.FontFamily,
            Foreground = style.Foreground,
            Padding = style.Padding
        };

        var border = new Border
        {
            Child = tb,
            Background = style.Background,
            BorderBrush = style.BorderBrush,
            BorderThickness = style.BorderThickness,
            CornerRadius = style.CornerRadius
        };

        return new InlineUIContainer(border)
        {
            BaselineAlignment = BaselineAlignment.Center
        };
    }
}
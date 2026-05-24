namespace MarkdigWpfViewer.Presenters;

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
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
            typeof(Style),
            typeof(MdTextBlock),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnAnyPropertyChanged));

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

    public Style InlineCodeStyle
    {
        get => (Style)GetValue(InlineCodeStyleProperty);
        set => SetValue(InlineCodeStyleProperty, value);
    }

    protected static void OnAnyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((MdTextBlock)d).Rebuild();

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
        var presenter = new InlineCodePresenter
        {
            Text = text,
            Style = InlineCodeStyle
        };

        return new InlineUIContainer(presenter)
        {
            BaselineAlignment = BaselineAlignment.Center
        };
    }
}
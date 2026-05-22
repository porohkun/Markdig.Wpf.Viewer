namespace MarkdigWpfViewer.Presenters;

using System.Windows;
using System.Windows.Controls;
using Styling;
using ViewModels;

public sealed class MdTablePresenter : ContentControl
{
    public static readonly DependencyProperty TableProperty =
        DependencyProperty.Register(
            nameof(Table),
            typeof(TableVm),
            typeof(MdTablePresenter),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnAnyPropertyChanged));

    public static readonly DependencyProperty CellTemplateProperty =
        DependencyProperty.Register(
            nameof(CellTemplate),
            typeof(DataTemplate),
            typeof(MdTablePresenter),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnAnyPropertyChanged));

    public static readonly DependencyProperty HeaderTemplateProperty =
        DependencyProperty.Register(
            nameof(HeaderTemplate),
            typeof(DataTemplate),
            typeof(MdTablePresenter),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnAnyPropertyChanged));

    public static readonly DependencyProperty TableStyleProperty =
        DependencyProperty.Register(
            nameof(TableStyle),
            typeof(MdTableStyle),
            typeof(MdTablePresenter),
            new FrameworkPropertyMetadata(new MdTableStyle(), OnAnyPropertyChanged));

    public MdTablePresenter()
    {
        Build();
    }

    public TableVm? Table
    {
        get => (TableVm?)GetValue(TableProperty);
        set => SetValue(TableProperty, value);
    }

    public DataTemplate CellTemplate
    {
        get => (DataTemplate)GetValue(CellTemplateProperty);
        set => SetValue(CellTemplateProperty, value);
    }

    public DataTemplate HeaderTemplate
    {
        get => (DataTemplate)GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }

    public MdTableStyle TableStyle
    {
        get => (MdTableStyle)GetValue(TableStyleProperty);
        set => SetValue(TableStyleProperty, value);
    }

    private static void OnAnyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((MdTablePresenter)d).Build();

    private void Build()
    {
        if (Table is null)
        {
            Content = null;
            return;
        }

        var grid = new Grid
        {
            SnapsToDevicePixels = true
        };

        for (var c = 0; c < Table.ColumnCount; c++)
        {
            grid.ColumnDefinitions.Add(new() { Width = GridLength.Auto });
        }

        for (var r = 0; r < Table.Rows.Count; r++)
        {
            grid.RowDefinitions.Add(new() { Height = GridLength.Auto });

            var row = Table.Rows[r];
            for (var c = 0; c < Table.ColumnCount; c++)
            {
                var cell = c < row.Cells.Count ? row.Cells[c] : new(TableStyle, []);

                var presenter = new ContentPresenter
                {
                    Content = cell,
                    ContentTemplate = row.IsHeader ? HeaderTemplate : CellTemplate
                };

                Grid.SetRow(presenter, r);
                Grid.SetColumn(presenter, c);
                grid.Children.Add(presenter);
            }
        }

        Content = grid;
    }
}
namespace MarkdigWpfViewer.Styling;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Abstractions;

public static class MarkdownStyling
{
    public static readonly DependencyProperty StyleProviderProperty =
        DependencyProperty.RegisterAttached(
            "StyleProvider",
            typeof(IMdStyleProvider),
            typeof(MarkdownStyling),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.Inherits,
                OnStyleProviderChanged));

    public static void SetStyleProvider(DependencyObject element, IMdStyleProvider? value)
        => element.SetValue(StyleProviderProperty, value);

    public static IMdStyleProvider? GetStyleProvider(DependencyObject element)
        => (IMdStyleProvider?)element.GetValue(StyleProviderProperty);

    private static void OnStyleProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement root)
            return;

        if (!root.IsLoaded)
        {
            root.Loaded += OnRootLoaded;
            return;
        }

        RefreshTemplates(root);
    }

    private static void OnRootLoaded(object sender, RoutedEventArgs e)
    {
        var root = (FrameworkElement)sender;
        root.Loaded -= OnRootLoaded;
        RefreshTemplates(root);
    }

    private static void RefreshTemplates(DependencyObject root)
    {
        foreach (var itemsControl in FindDescendants<ItemsControl>(root))
        {
            var selector = itemsControl.ItemTemplateSelector;
            if (selector is null)
                continue;

            itemsControl.ClearValue(ItemsControl.ItemTemplateSelectorProperty);
            itemsControl.SetCurrentValue(ItemsControl.ItemTemplateSelectorProperty, selector);

            if (CollectionViewSource.GetDefaultView(itemsControl.ItemsSource) is ICollectionView view)
                view.Refresh();
        }
    }

    private static IEnumerable<T> FindDescendants<T>(DependencyObject root) where T : DependencyObject
    {
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
        {
            var child = VisualTreeHelper.GetChild(root, i);

            if (child is T typed)
                yield return typed;

            foreach (var nested in FindDescendants<T>(child))
                yield return nested;
        }
    }
}
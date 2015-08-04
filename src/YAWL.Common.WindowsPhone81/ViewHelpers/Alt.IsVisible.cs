using Windows.UI.Xaml;

namespace YAWL.Common.ViewHelpers
{
    public partial class Alt
    {
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached(
            "IsVisible", typeof(object), typeof(Alt), new PropertyMetadata(default(object), IsVisibleChangedCallback));

        private static void IsVisibleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fe = d as FrameworkElement;
            if (fe == null)
                return;

            fe.Visibility = e.NewValue is bool && (bool)e.NewValue
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public static void SetIsVisible(DependencyObject element, object value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static object GetIsVisible(DependencyObject element)
        {
            return element.GetValue(IsVisibleProperty);
        }
    }

    public partial class Alt
    {
        public static readonly DependencyProperty IsNotVisibleProperty = DependencyProperty.RegisterAttached(
            "IsNotVisible", typeof(object), typeof(Alt), new PropertyMetadata(default(object), IsNotVisibleChangedCallback));

        private static void IsNotVisibleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fe = d as FrameworkElement;
            if (fe == null)
                return;

            fe.Visibility = e.NewValue is bool && (bool)e.NewValue
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        public static void SetIsNotVisible(DependencyObject element, object value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static object GetIsNotVisible(DependencyObject element)
        {
            return element.GetValue(IsNotVisibleProperty);
        }
    }
}

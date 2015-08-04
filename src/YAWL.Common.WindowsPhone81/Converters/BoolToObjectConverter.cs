using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace YAWL.Common.Converters
{
    public class BoolToObjectConverter : DependencyObject, IValueConverter
    {
        #region TrueValue dependency property
        public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register(
            "TrueValue", typeof(object), typeof(BoolToObjectConverter), new PropertyMetadata(default(object)));

        public object TrueValue
        {
            get { return GetValue(TrueValueProperty); }
            set { SetValue(TrueValueProperty, value); }
        }
        #endregion

        #region FalseValue dependency property
        public static readonly DependencyProperty FalseValueProperty = DependencyProperty.Register(
            "FalseValue", typeof(object), typeof(BoolToObjectConverter), new PropertyMetadata(default(object)));

        public object FalseValue
        {
            get { return GetValue(FalseValueProperty); }
            set { SetValue(FalseValueProperty, value); }
        }
        #endregion

        public virtual object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && (bool)value)
            {
                return TrueValue;
            }

            return FalseValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value == TrueValue;
        }
    }
}

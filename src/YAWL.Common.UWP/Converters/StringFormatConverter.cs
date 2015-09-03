// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace YAWL.Common.Converters
{
    public class StringFormatConverter : DependencyObject, IValueConverter
    {
        #region Format dependency property
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register(
            "Format", typeof(string), typeof(StringFormatConverter), new PropertyMetadata(default(string)));

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }
        #endregion

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format(parameter as string
                                 ?? Format.Replace("\\", string.Empty)
                                 ?? "{0}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
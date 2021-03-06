﻿// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;
using Windows.UI.Xaml.Data;

namespace YAWL.Common.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = value as bool?;
            return boolValue != true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var boolValue = value as bool?;
            return boolValue != true;
        }
    }
}
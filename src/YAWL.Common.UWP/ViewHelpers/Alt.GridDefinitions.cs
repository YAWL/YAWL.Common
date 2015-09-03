using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace YAWL.Common.ViewHelpers
{
    public static class DefinitionsExtensions
    {
        /// <summary>
        /// Fills or clears row definitions based on the specified format.
        /// If the format is null or whitespace, the row definition collection
        /// is cleared. Otherwise, it is parsed and definitions are added.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="format">Comma separated row definitions.</param>
        /// <example>This sample shows you how to call <see cref="ParseAndFill"/> method.
        /// <code>
        /// grid.RowDefinitions.ParseAndFill("Auto,*,2*,10");</code></example>
        /// <exception cref="System.NullReferenceException">If @this is null.</exception>
        public static void ParseAndFill(this RowDefinitionCollection @this, string format)
        {
            if (@this == null)
                throw new NullReferenceException("@this must not be null");

            @this.Clear();

            // this is still valid, definition should be empty
            if (string.IsNullOrWhiteSpace(format))
                return;

            foreach (var unit in ParseFormat(format))
                Add(@this, unit);
        }

        /// <summary>
        /// Fills or clears column definitions based on the specified format.
        /// If the format is null or whitespace, the column definition collection
        /// is cleared. Otherwise, it is parsed and definitions are added.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="format">Comma separated row definitions.</param>
        /// <example>This sample shows you how to call <see cref="ParseAndFill"/> method.
        /// <code>
        /// grid.ColumnDefinitions.ParseAndFill("Auto,*,2*,10");</code></example>
        /// <exception cref="System.NullReferenceException">If @this is null.</exception>
        public static void ParseAndFill(this ColumnDefinitionCollection @this, string format)
        {
            if (@this == null)
                throw new NullReferenceException("@this must not be null");

            @this.Clear();

            // this is still valid, definition should be empty
            if (string.IsNullOrWhiteSpace(format))
                return;

            foreach (var unit in ParseFormat(format))
                Add(@this, unit);
        }

        internal static IEnumerable<GridLength> ParseFormat(string format)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return ParseFormatImplementation(format);
        }

        internal static IEnumerable<GridLength> ParseFormatImplementation(string format)
        {
            var values = format.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim())
                .ToArray();
            foreach (var value in values)
            {
                if (value.Equals("Auto", StringComparison.CurrentCultureIgnoreCase))
                {
                    yield return new GridLength(1, GridUnitType.Auto);
                }
                else if (value.Equals("*"))
                {
                    yield return new GridLength(1, GridUnitType.Star);
                }
                else if (value.EndsWith("*"))
                {
                    double d;
                    if (double.TryParse(value.Substring(0, value.Length - 1), out d))
                        yield return new GridLength(d, GridUnitType.Star);
                }
                else
                {
                    double d;
                    if (double.TryParse(value, out d))
                        yield return new GridLength(d, GridUnitType.Pixel);
                }
            }
        }

        internal static void Add(this RowDefinitionCollection @this, GridLength length)
        {
            if (@this == null)
                throw new NullReferenceException("@this must not be null");

            @this.Add(new RowDefinition
            {
                Height = length
            });
        }

        internal static void Add(this ColumnDefinitionCollection @this, GridLength length)
        {
            if (@this == null)
                throw new NullReferenceException("@this must not be null");

            @this.Add(new ColumnDefinition
            {
                Width = length
            });
        }
    }


    public partial class Alt
    {
        #region RowDefinitions attached property
        public static string GetRowDefinitions(DependencyObject obj)
        {
            return (string)obj.GetValue(RowDefinitionsProperty);
        }

        public static void SetRowDefinitions(DependencyObject obj, string value)
        {
            obj.SetValue(RowDefinitionsProperty, value);
        }

        public static readonly DependencyProperty RowDefinitionsProperty =
            DependencyProperty.RegisterAttached("RowDefinitions",
                                                typeof(string),
                                                typeof(Grid),
                                                new PropertyMetadata(string.Empty,
                                                                     RowDefinitions_PropertyChangedCallback));

        private static void RowDefinitions_PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var grid = o as Grid;
            if (grid != null)
                grid.RowDefinitions.ParseAndFill(args.NewValue as string);
        }
        #endregion

        #region ColumnDefinitions attached property
        public static string GetColumnDefinitions(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumnDefinitionsProperty);
        }

        public static void SetColumnDefinitions(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnDefinitionsProperty, value);
        }

        public static readonly DependencyProperty ColumnDefinitionsProperty =
            DependencyProperty.RegisterAttached("ColumnDefinitions",
                                                typeof(string),
                                                typeof(Grid),
                                                new PropertyMetadata(string.Empty,
                                                                     ColumnDefinitions_PropertyChangedCallback));

        private static void ColumnDefinitions_PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var grid = o as Grid;
            if (grid != null)
                grid.ColumnDefinitions.ParseAndFill(args.NewValue as string);
        }
        #endregion
    }
}

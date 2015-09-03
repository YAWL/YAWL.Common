// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace YAWL.Common.Mvvm
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Property helpers
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected TReturn Get<TReturn>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            object o;
            if (!_values.TryGetValue(propertyName, out o) ||
                o == null)
                return default(TReturn);

            if (o is TReturn)
                return (TReturn)o;

            throw new InvalidOperationException(
                string.Format("Invalid type for property {0}. Requested {1}, remembered {2}",
                    propertyName,
                    typeof(TReturn).FullName,
                    o.GetType().FullName));
        }
        protected bool Set<TReturn>(TReturn value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            object existing;
            if (!_values.TryGetValue(propertyName, out existing))
            {
                _values.Add(propertyName, value);
                // ReSharper disable once ExplicitCallerInfoArgument
                RaisePropertyChanged(propertyName);
                return true;
            }

            // existing value for this property is not null and types differ
            // in case of value types, check for nullable
            var newType = typeof(TReturn);
            var newTypeInfo = newType.GetTypeInfo();

            // ReSharper disable once CompareNonConstrainedGenericWithNull
            // it's ok, we checked if it was a value type before null comparison
            if (!newTypeInfo.IsValueType && value != null)
                newType = value.GetType();

            if (existing != null &&
                newType != existing.GetType() &&
                // Toni: fixed type comparison from silverlight to portable
                !newTypeInfo.IsAssignableFrom(typeof(TReturn).GetTypeInfo()) &&
                //!newType.IsAssignableFrom(typeof(TReturn)) &&
                (!newTypeInfo.IsValueType ||
                 newType != typeof(Nullable<>).MakeGenericType(existing.GetType())))
            {
                throw new InvalidOperationException(
                    string.Format("Invalid type for property {0}. Saving {1}, previous {2}. Property type is {3}",
                        propertyName,
                        newType.FullName,
                        existing.GetType().FullName,
                        typeof(TReturn).FullName));
            }

            if (EqualityComparer<TReturn>.Default.Equals((TReturn)existing, value))
                return false;

            _values[propertyName] = value;
            // ReSharper disable once ExplicitCallerInfoArgument
            RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace YAWL.Common.Mvvm
{
    public static class ObservablePropertyHelpers
    {
        public static ObservableBoolProperty OnChange<T>(this ObservableCollection<T> collection,
            Func<IEnumerable<T>, bool> predicate, bool defaultValue = false)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var property = new ObservableBoolProperty(defaultValue);

            collection.CollectionChanged += (sender, args) =>
            {
                property.Value = predicate(collection);
            };

            return property;
        }

        public static ObservableProperty<TResult> OnChange<TInput, TResult>(
            this ObservableCollection<TInput> collection, Func<IEnumerable<TInput>, TResult> evaluator,
            TResult defaultValue = default (TResult))
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (evaluator == null)
                throw new ArgumentNullException("evaluator");

            var property = new ObservableProperty<TResult>(defaultValue);

            collection.CollectionChanged += (sender, args) =>
            {
                property.Value = evaluator(collection);
            };

            return property;
        }
    }
}

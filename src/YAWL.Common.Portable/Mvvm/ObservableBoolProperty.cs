using System.Collections.Generic;
using System.ComponentModel;

namespace YAWL.Common.Mvvm
{
    public class ObservableBoolProperty : INotifyPropertyChanged
    {
        // ReSharper disable once StaticFieldInGenericType
        private static readonly PropertyChangedEventArgs ValueEventArgs = new PropertyChangedEventArgs("Value");
        private static readonly PropertyChangedEventArgs InverseEventArgs = new PropertyChangedEventArgs("Inverse");

        private bool _value;

        public bool Value
        {
            get { return _value; }
            set
            {
                if (EqualityComparer<bool>.Default.Equals(_value, value))
                    return;

                _value = value;
                RaisePropertyChanged();
            }
        }

        public bool Inverse
        {
            get { return !_value; }
            set
            {
                if (EqualityComparer<bool>.Default.Equals(!_value, value))
                    return;

                _value = !value;
                RaisePropertyChanged();
            }
        }

        public ObservableBoolProperty(bool val = false)
        {
            _value = val;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        protected void RaisePropertyChanged()
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, ValueEventArgs);
                handler(this, InverseEventArgs);
            }
        }

        public static implicit operator bool(ObservableBoolProperty property)
        {
            return property.Value;
        }

        #region Operators

        /// <summary>
        /// Returns a new observable property that has an inverse value and is always bound to the source property.
        /// All changes to the original property are reflected in the new one as well.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ObservableBoolProperty operator !(ObservableBoolProperty source)
        {
            var newProperty = new ObservableBoolProperty(source.Inverse);
            source.PropertyChanged += (sender, args) => newProperty.Value = source.Inverse;
            return newProperty;
        }

        public static ObservableBoolProperty operator||(ObservableBoolProperty first, ObservableBoolProperty second)
    {
        
    }

        #endregion
    }
}

using System.Collections.Generic;
using System.ComponentModel;

namespace YAWL.Common.Mvvm
{
    public class ObservableProperty<T> : INotifyPropertyChanged
    {
        // ReSharper disable once StaticFieldInGenericType
        private static readonly PropertyChangedEventArgs EventArgs = new PropertyChangedEventArgs("Value");

        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, EventArgs);
                }
            }
        }

        public ObservableProperty(T val = default(T))
        {
            _value = val;
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : string.Empty;
        }

        //public static implicit operator ObservableProperty<T>(T val)
        //{
        //    return new ObservableProperty<T>(val);
        //}

        public static implicit operator T(ObservableProperty<T> prop)
        {
            return prop.Value;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}

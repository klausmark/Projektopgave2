using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HowToBind
{
    public class NotifyBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _backingFields = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual T GetField<T>([CallerMemberName] string propertyName = "")
        {
            return GetBackingField<T>(propertyName);
        }

        private T GetBackingField<T>(string propertyName, T defaultValue = default(T))
        {
            if (!_backingFields.ContainsKey(propertyName)) return defaultValue;
            return (T)_backingFields[propertyName];
        }

        protected virtual void SetField<T>(T value, [CallerMemberName] string propertyName = "")
        {
            if (BackingFieldDoesNotContain(propertyName))
            {
                SetBackingField(value, propertyName);
                return;
            }

            if (ValueChanged(value, propertyName))
            {
                SetBackingField(value, propertyName);
            }
        }

        private bool BackingFieldDoesNotContain(string propertyName)
        {
            return !_backingFields.ContainsKey(propertyName);
        }

        private bool ValueChanged<T>(T value, string propertyName)
        {
            var oldValue = _backingFields[propertyName];
            if (oldValue == null && value == null) return false;
            if (oldValue == null) return true;
            return !oldValue.Equals(value);
        }

        private void SetBackingField<T>(T value, string propertyName)
        {
            _backingFields[propertyName] = value;
            OnPropertyChanged(propertyName);
        }
    }
}

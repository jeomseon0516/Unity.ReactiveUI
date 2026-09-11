using System;
using Jeomseon.Unity.Reactive.ReactiveField;
using Unity.Properties;
using UnityEngine.UIElements;

namespace Jeomseon.Unity.ReactiveUI.Binding
{
    /// <summary>Adapts a writable reactive field to a Unity UI Toolkit runtime data source.</summary>
    public sealed class ReactiveFieldDataSource<T> : INotifyBindablePropertyChanged, IDisposable
    {
        private readonly IReactiveField<T> _field;
        private bool _disposed;

        public ReactiveFieldDataSource(IReactiveField<T> field)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _field.AddListenerWithoutNotify(OnValueChanged);
        }

        public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

        [CreateProperty]
        public T Value
        {
            get => _field.Value;
            set
            {
                ThrowIfDisposed();
                _field.Value = value;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _field.ChangedEvent -= OnValueChanged;
            _disposed = true;
        }

        private void OnValueChanged(T _) =>
            propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(nameof(Value)));

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ReactiveFieldDataSource<T>));
        }
    }
}

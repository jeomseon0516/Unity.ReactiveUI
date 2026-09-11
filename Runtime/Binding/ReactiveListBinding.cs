using System;
using Jeomseon.Unity.Reactive.ReactiveList;
using UnityEngine.UIElements;

namespace Jeomseon.Unity.ReactiveUI.Binding
{
    /// <summary>Keeps a UI Toolkit collection view synchronized with a reactive list.</summary>
    public sealed class ReactiveListBinding<T> : IDisposable
    {
        private readonly BaseVerticalCollectionView _view;
        private readonly ReactiveList<T> _list;
        private bool _disposed;

        public ReactiveListBinding(BaseVerticalCollectionView view, ReactiveList<T> list)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _list = list ?? throw new ArgumentNullException(nameof(list));
            _view.itemsSource = list;
            _list.AddListenerToAddedEventWithoutNotify(OnItemsAdded);
            _list.RemovedEvent += OnItemsRemoved;
            _list.ChangedEvent += OnItemChanged;
            _list.ReorderedEvent += OnItemsReordered;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _list.AddedEvent -= OnItemsAdded;
            _list.RemovedEvent -= OnItemsRemoved;
            _list.ChangedEvent -= OnItemChanged;
            _list.ReorderedEvent -= OnItemsReordered;
            _disposed = true;
        }

        private void OnItemsAdded(int[] _, T[] __) => _view.RefreshItems();
        private void OnItemsRemoved(int[] _, T[] __) => _view.RefreshItems();
        private void OnItemChanged(int _, T __, T ___) => _view.RefreshItems();
        private void OnItemsReordered(System.Collections.Generic.IReadOnlyList<T> _) => _view.RefreshItems();
    }
}

# Basic Usage

Open `ReactiveUIBasicUsage.unity` and enter Play Mode.

- Edit **Reactive value** or press **Increment** to exercise the two-way `ReactiveField<int>` binding.
- Press **Add list entry** to append to a `ReactiveList<string>` and refresh the `ListView`.
- Stop and restart Play Mode with Domain Reload disabled to verify that the disposable bindings do not leave subscriptions behind.

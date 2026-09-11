# Jeomseon Unity Reactive UI

[한국어](./README.md) | English

A thin adapter from `Jeomseon.Unity.Reactive` state to Unity 6000.6 UI Toolkit Runtime Data Binding.
It does not reimplement MVVM, commands, DI, Redux, or widgets already supplied by App UI.

## 0.1 scope

- `ReactiveFieldDataSource<T>` adapts `ReactiveField<T>` to `INotifyBindablePropertyChanged`.
- `ReactiveListBinding<T>` synchronizes `ReactiveList<T>` with `BaseVerticalCollectionView`.
- Subscription lifetime is explicit through `IDisposable`.

## Installation

Install with the OpenUPM CLI:

```bash
openupm add com.jeomseon.unity.reactive-ui
```

For a Git URL installation, use this URL in Package Manager:

```text
https://github.com/jeomseon0516/Unity.ReactiveUI.git?path=/
```

## ReactiveField binding

The adapter exposes `Value` through `[CreateProperty]` and notifies UI Toolkit when the field changes.

```csharp
var health = new ReactiveField<int>(100);
var source = new ReactiveFieldDataSource<int>(health);

healthField.dataSource = source;
healthField.SetBinding(nameof(IntegerField.value), new DataBinding
{
    bindingMode = BindingMode.TwoWay,
    dataSourcePath = PropertyPath.FromName(nameof(source.Value))
});
```

## ReactiveList binding

```csharp
var entries = new ReactiveList<string>();
var binding = new ReactiveListBinding<string>(listView, entries);

entries.Add("First");
entries[0] = "Updated";
```

Dispose both adapters from the owning component's `OnDisable` or `OnDestroy`. Import the **Basic Usage**
Sample from Package Manager for an executable example.

## Boundary

The only required dependency is `Jeomseon.Unity.Reactive`. The package can be combined with App UI and
`Jeomseon.Unity.UI`, but requires neither. It does not provide view models, commands, DI, Redux, navigation,
or widgets.

# Jeomseon Unity Reactive UI

[한국어](README.md) | English

Connects Reactive values to UI Toolkit runtime DataBinding and ReactiveList to collection view itemsSource.

## Quick start

Create a ReactiveFieldDataSource and assign UI dataSource/Binding, or pass a view/list to ReactiveListBinding. Changes notify/RefreshItems; the owner must Dispose.

## Contracts and limitations

Dispose unsubscribes listeners but does not clear itemsSource/dataSource references. Clear those when reusing views. List changes use RefreshItems rather than a diff-based optimization.

## Requirements

- Unity `6000.6.0f1`
- Package: `com.jeomseon.unity.reactive-ui` `0.1.0`
- `com.jeomseon.unity.reactive`: `0.4.2`

## Installation

Merge this into your project's `Packages/manifest.json`; preserve existing dependencies and registries.

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.jeomseon"
      ]
    },
    {
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.openupm.com",
      "scopes": [
        "org.nuget"
      ]
    }
  ],
  "dependencies": {
    "com.jeomseon.unity.reactive-ui": "0.1.0"
  }
}
```

Or use **Install package from git URL** in Package Manager:

```text
https://github.com/jeomseon0516/Unity.ReactiveUI.git#v0.1.0
```

Keep the Unity NuGet registry when installing by Git URL too; Reactive uses ObservableCollections.

## API usage and details

### 0.1 scope

- `ReactiveFieldDataSource<T>` adapts `ReactiveField<T>` to `INotifyBindablePropertyChanged`.
- `ReactiveListBinding<T>` synchronizes `ReactiveList<T>` with `BaseVerticalCollectionView`.
- Subscription lifetime is explicit through `IDisposable`.

### Installation

Install with the OpenUPM CLI:

```bash
openupm add com.jeomseon.unity.reactive-ui
```

For a Git URL installation, use this URL in Package Manager:

```text
https://github.com/jeomseon0516/Unity.ReactiveUI.git?path=/
```

### ReactiveField binding

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

### ReactiveList binding

```csharp
var entries = new ReactiveList<string>();
var binding = new ReactiveListBinding<string>(listView, entries);

entries.Add("First");
entries[0] = "Updated";
```

Dispose both adapters from the owning component's `OnDisable` or `OnDestroy`. Import the **Basic Usage**
Sample from Package Manager for an executable example.

### Boundary

The only required dependency is `Jeomseon.Unity.Reactive`. The package can be combined with App UI and
`Jeomseon.Unity.UI`, but requires neither. It does not provide view models, commands, DI, Redux, navigation,
or widgets.

## Samples

Import the desired entry from **Package Manager > Samples**. Use the included Scene/Editor tool and complete its Setup step first, if required.

- [Basic Usage](Samples~/BasicUsage/README.md)
  - [ReactiveUIBasicUsage](Samples~/BasicUsage/ReactiveUIBasicUsage.unity)

## Tests

[Test sources](Tests)

Add the package ID to manifest `testables`, then execute its relevant EditMode/PlayMode assemblies in Unity Test Runner.

## Further reading

[Complete source map and package metadata](Documentation~/Overview.en.md)
- [Changelog](CHANGELOG.md)

## License

[License](LICENSE.md)

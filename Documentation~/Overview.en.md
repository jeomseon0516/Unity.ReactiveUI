# Jeomseon Unity Reactive UI

[한국어](Overview.md) · [Installation](../README.en.md)

## Purpose

Connects Reactive values to UI Toolkit runtime DataBinding and ReactiveList to collection view itemsSource.

## Usage flow

Create a ReactiveFieldDataSource and assign UI dataSource/Binding, or pass a view/list to ReactiveListBinding. Changes notify/RefreshItems; the owner must Dispose.

## Contracts and limitations

Dispose unsubscribes listeners but does not clear itemsSource/dataSource references. Clear those when reusing views. List changes use RefreshItems rather than a diff-based optimization.

## Package requirements

- Package ID: `com.jeomseon.unity.reactive-ui`
- Version: `0.1.0`
- Unity: `6000.6` / `0f1`
- `com.jeomseon.unity.reactive`: `0.4.2`

## Samples and verification

Import the following from Package Manager > Samples. Open the included Scene or Editor tool; follow any Setup step in its sample README.

- [Basic Usage](../Samples~/BasicUsage/README.md)
  - [ReactiveUIBasicUsage](../Samples~/BasicUsage/ReactiveUIBasicUsage.unity)

[Test sources](../Tests)

Add the package ID to manifest `testables`, then run the relevant Unity Test Runner assemblies. These links describe available tests, not a new successful test run.

## Source map

- [ReactiveFieldDataSource](../Runtime/Binding/ReactiveFieldDataSource.cs)
- [ReactiveListBinding](../Runtime/Binding/ReactiveListBinding.cs)

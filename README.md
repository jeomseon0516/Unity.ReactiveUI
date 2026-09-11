# Jeomseon Unity Reactive UI

한국어 | [English](./README.en.md)

`Jeomseon.Unity.Reactive`의 상태를 Unity 6000.6 UI Toolkit Runtime Data Binding에 연결하는 얇은 어댑터 패키지입니다.
App UI가 이미 제공하는 MVVM, Command, DI, Redux, 위젯을 재구현하지 않습니다.

## 0.1 범위

- `ReactiveFieldDataSource<T>`: `ReactiveField<T>`를 `INotifyBindablePropertyChanged` 데이터 소스로 연결
- `ReactiveListBinding<T>`: `ReactiveList<T>`를 `BaseVerticalCollectionView`와 동기화
- 구독 수명은 `IDisposable`로 명시적으로 관리

## 설치

OpenUPM CLI로 설치합니다.

```bash
openupm add com.jeomseon.unity.reactive-ui
```

Git URL로 설치할 때는 Package Manager에서 다음 주소를 사용합니다.

```text
https://github.com/jeomseon0516/Unity.ReactiveUI.git?path=/
```

## ReactiveField 바인딩

어댑터의 `Value`는 `[CreateProperty]`로 공개되며 필드 변경 시 UI Toolkit에 속성 변경을 알립니다.

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

## ReactiveList 바인딩

```csharp
var entries = new ReactiveList<string>();
var binding = new ReactiveListBinding<string>(listView, entries);

entries.Add("First");
entries[0] = "Updated";
```

두 어댑터는 소유 컴포넌트의 `OnDisable` 또는 `OnDestroy`에서 `Dispose()`합니다. 자세한 실행 예제는
Package Manager에서 **Basic Usage** Sample을 Import해 확인할 수 있습니다.

## 설계 경계

필수 의존성은 `Jeomseon.Unity.Reactive`뿐입니다. App UI 및 `Jeomseon.Unity.UI`와 함께 사용할 수 있지만
어느 쪽도 강제하지 않습니다. ViewModel, Command, DI, Redux, Navigation과 위젯은 제공하지 않습니다.

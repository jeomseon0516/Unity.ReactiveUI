# Jeomseon Unity Reactive UI

한국어 | [English](README.en.md)

Reactive 값을 UI Toolkit 런타임 DataBinding에, ReactiveList를 collection view itemsSource에 연결합니다.

## 빠르게 시작하기

ReactiveFieldDataSource를 만들고 UI dataSource/Binding에 연결하거나 ReactiveListBinding에 view와 list 전달 → 변경 시 알림/RefreshItems → 소유자가 Dispose.

## 계약과 주의사항

Dispose는 listener를 해제하며 view.itemsSource나 dataSource 참조를 자동 삭제하지 않습니다. View를 재사용할 때 참조도 정리하세요. 목록 변경은 전체 RefreshItems를 수행하며 diff 기반 최적화는 아닙니다.

## 요구 사항

- Unity `6000.6.0f1`
- 패키지: `com.jeomseon.unity.reactive-ui` `0.1.0`
- `com.jeomseon.unity.reactive`: `0.4.2`

## 설치

프로젝트 `Packages/manifest.json`의 기존 dependencies와 registry를 유지하면서 다음 항목을 병합합니다.

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

또는 Package Manager의 **Install package from git URL**을 사용합니다:

```text
https://github.com/jeomseon0516/Unity.ReactiveUI.git#v0.1.0
```

Git URL로 설치해도 Reactive의 ObservableCollections 의존성을 위해 Unity NuGet registry를 유지합니다.

## API 사용과 상세 설명

### 0.1 범위

- `ReactiveFieldDataSource<T>`: `ReactiveField<T>`를 `INotifyBindablePropertyChanged` 데이터 소스로 연결
- `ReactiveListBinding<T>`: `ReactiveList<T>`를 `BaseVerticalCollectionView`와 동기화
- 구독 수명은 `IDisposable`로 명시적으로 관리

### 설치

OpenUPM CLI로 설치합니다.

```bash
openupm add com.jeomseon.unity.reactive-ui
```

Git URL로 설치할 때는 Package Manager에서 다음 주소를 사용합니다.

```text
https://github.com/jeomseon0516/Unity.ReactiveUI.git?path=/
```

### ReactiveField 바인딩

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

### ReactiveList 바인딩

```csharp
var entries = new ReactiveList<string>();
var binding = new ReactiveListBinding<string>(listView, entries);

entries.Add("First");
entries[0] = "Updated";
```

두 어댑터는 소유 컴포넌트의 `OnDisable` 또는 `OnDestroy`에서 `Dispose()`합니다. 자세한 실행 예제는
Package Manager에서 **Basic Usage** Sample을 Import해 확인할 수 있습니다.

### 설계 경계

필수 의존성은 `Jeomseon.Unity.Reactive`뿐입니다. App UI 및 `Jeomseon.Unity.UI`와 함께 사용할 수 있지만
어느 쪽도 강제하지 않습니다. ViewModel, Command, DI, Redux, Navigation과 위젯은 제공하지 않습니다.

## 샘플 실행

**Package Manager > Samples**에서 예제를 Import합니다. 포함된 Scene 또는 Editor 도구를 열고 필요한 Setup 메뉴를 먼저 실행합니다.

- [Basic Usage](Samples~/BasicUsage/README.md)
  - [ReactiveUIBasicUsage](Samples~/BasicUsage/ReactiveUIBasicUsage.unity)

## 테스트

[테스트 코드](Tests)

manifest의 `testables`에 패키지 ID를 추가하고 Unity Test Runner의 관련 EditMode/PlayMode 어셈블리를 실행합니다.

## 추가 문서

[전체 코드 지도와 패키지 구성](Documentation~/Overview.md)
- [Changelog](CHANGELOG.md)

## 라이선스

[License](LICENSE.md)

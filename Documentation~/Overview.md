# Jeomseon Unity Reactive UI

[English](Overview.en.md) · [설치](../README.md)

## 목적

Reactive 값을 UI Toolkit 런타임 DataBinding에, ReactiveList를 collection view itemsSource에 연결합니다.

## 사용 흐름

ReactiveFieldDataSource를 만들고 UI dataSource/Binding에 연결하거나 ReactiveListBinding에 view와 list 전달 → 변경 시 알림/RefreshItems → 소유자가 Dispose.

## 계약과 제한

Dispose는 listener를 해제하며 view.itemsSource나 dataSource 참조를 자동 삭제하지 않습니다. View를 재사용할 때 참조도 정리하세요. 목록 변경은 전체 RefreshItems를 수행하며 diff 기반 최적화는 아닙니다.

## 패키지 요구 사항

- Package ID: `com.jeomseon.unity.reactive-ui`
- Version: `0.1.0`
- Unity: `6000.6` / `0f1`
- `com.jeomseon.unity.reactive`: `0.4.2`

## 샘플과 검증

Package Manager > Samples에서 다음 예제를 Import하세요. 포함된 Scene 또는 Editor 도구를 열고 샘플 README의 Setup 절차를 먼저 수행합니다.

- [Basic Usage](../Samples~/BasicUsage/README.md)
  - [ReactiveUIBasicUsage](../Samples~/BasicUsage/ReactiveUIBasicUsage.unity)

[테스트 코드](../Tests)

소비 프로젝트 manifest의 `testables`에 패키지 ID를 추가하고 Unity Test Runner의 관련 어셈블리를 실행합니다. 위 링크는 테스트 위치이며 이번 문서 수정으로 새 테스트 실행을 주장하지 않습니다.

## 코드 지도

- [ReactiveFieldDataSource](../Runtime/Binding/ReactiveFieldDataSource.cs)
- [ReactiveListBinding](../Runtime/Binding/ReactiveListBinding.cs)

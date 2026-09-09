using Jeomseon.Unity.Reactive.ReactiveField;
using Jeomseon.Unity.Reactive.ReactiveList;
using Jeomseon.Unity.ReactiveUI.Binding;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Jeomseon.Unity.ReactiveUI.Samples.BasicUsage
{
    public sealed class ReactiveUIBasicUsageSample : MonoBehaviour
    {
        private readonly ReactiveField<int> _counter = new();
        private readonly ReactiveList<string> _entries = new();

        private ReactiveFieldDataSource<int> _counterSource;
        private ReactiveListBinding<string> _listBinding;
        private PanelSettings _runtimePanelSettings;

        private void Awake()
        {
            var document = gameObject.AddComponent<UIDocument>();
            _runtimePanelSettings = ScriptableObject.CreateInstance<PanelSettings>();
            _runtimePanelSettings.themeStyleSheet = Resources.Load<ThemeStyleSheet>("ReactiveUIDefaultTheme");
            document.panelSettings = _runtimePanelSettings;

            _counterSource = new ReactiveFieldDataSource<int>(_counter);
            BuildUI(document.rootVisualElement);
        }

        private void BuildUI(VisualElement root)
        {
            root.style.paddingLeft = 24;
            root.style.paddingRight = 24;
            root.style.paddingTop = 24;
            root.style.paddingBottom = 24;

            root.Add(new Label("Reactive UI Basic Usage") { style = { fontSize = 24 } });
            root.Add(new Label("Edit the value or use the buttons. Both update the same ReactiveField."));

            var valueField = new IntegerField("Reactive value");
            valueField.dataSource = _counterSource;
            valueField.SetBinding(nameof(IntegerField.value), new DataBinding
            {
                bindingMode = BindingMode.TwoWay,
                dataSourcePath = PropertyPath.FromName(nameof(_counterSource.Value))
            });
            root.Add(valueField);

            var incrementButton = new Button(() => _counter.Value++) { text = "Increment" };
            root.Add(incrementButton);

            var list = new ListView
            {
                headerTitle = "Reactive entries",
                showFoldoutHeader = true,
                fixedItemHeight = 24,
                style = { height = 220, marginTop = 12 },
                makeItem = () => new Label(),
                bindItem = (element, index) => ((Label)element).text = _entries[index]
            };
            _listBinding = new ReactiveListBinding<string>(list, _entries);
            root.Add(list);

            root.Add(new Button(() => _entries.Add($"Entry {_entries.Count + 1}")) { text = "Add list entry" });
        }

        private void OnDestroy()
        {
            _listBinding?.Dispose();
            _counterSource?.Dispose();
            if (_runtimePanelSettings != null) Destroy(_runtimePanelSettings);
        }
    }
}

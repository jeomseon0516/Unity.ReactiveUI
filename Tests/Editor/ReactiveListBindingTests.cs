using System;
using Jeomseon.Unity.Reactive.ReactiveList;
using Jeomseon.Unity.ReactiveUI.Binding;
using NUnit.Framework;
using UnityEngine.UIElements;

namespace Jeomseon.Unity.ReactiveUI.Tests
{
    public sealed class ReactiveListBindingTests
    {
        [Test]
        public void Constructor_NullArguments_ThrowArgumentNullException()
        {
            var list = new ReactiveList<string>();
            Assert.Throws<ArgumentNullException>(() => new ReactiveListBinding<string>(null, list));
            Assert.Throws<ArgumentNullException>(() => new ReactiveListBinding<string>(new ListView(), null));
        }

        [Test]
        public void Constructor_AssignsReactiveListAsItemsSource()
        {
            var list = new ReactiveList<string>(new[] { "Alpha", "Beta" });
            var view = new ListView();

            using var binding = new ReactiveListBinding<string>(view, list);

            Assert.AreSame(list, view.itemsSource);
            Assert.AreEqual(2, view.itemsSource.Count);
            Assert.AreEqual("Alpha", view.itemsSource[0]);
        }

        [Test]
        public void Mutations_AreVisibleThroughItemsSource()
        {
            var list = new ReactiveList<string>();
            var view = new ListView();
            using var binding = new ReactiveListBinding<string>(view, list);

            list.Add("First");
            list[0] = "Updated";

            Assert.AreEqual(1, view.itemsSource.Count);
            Assert.AreEqual("Updated", view.itemsSource[0]);
        }

        [Test]
        public void Dispose_IsIdempotentAndAllowsLaterListChanges()
        {
            var list = new ReactiveList<int>();
            var binding = new ReactiveListBinding<int>(new ListView(), list);

            binding.Dispose();
            binding.Dispose();

            Assert.DoesNotThrow(() => list.Add(1));
        }
    }
}

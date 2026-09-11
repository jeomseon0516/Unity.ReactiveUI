using System;
using Jeomseon.Unity.Reactive.ReactiveField;
using Jeomseon.Unity.ReactiveUI.Binding;
using NUnit.Framework;

namespace Jeomseon.Unity.ReactiveUI.Tests
{
    public sealed class ReactiveFieldDataSourceTests
    {
        [Test]
        public void Constructor_NullField_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new ReactiveFieldDataSource<int>(null));

        [Test]
        public void FieldChange_RaisesValuePropertyNotification()
        {
            var field = new ReactiveField<int>();
            using var source = new ReactiveFieldDataSource<int>(field);
            string changedProperty = null;
            source.propertyChanged += (_, args) => changedProperty = args.propertyName.ToString();

            field.Value = 7;

            Assert.AreEqual(nameof(source.Value), changedProperty);
            Assert.AreEqual(7, source.Value);
        }

        [Test]
        public void ValueSetter_UpdatesReactiveField()
        {
            var field = new ReactiveField<int>();
            using var source = new ReactiveFieldDataSource<int>(field);
            source.Value = 11;
            Assert.AreEqual(11, field.Value);
        }

        [Test]
        public void Dispose_UnsubscribesAndRejectsWrites()
        {
            var field = new ReactiveField<int>();
            var source = new ReactiveFieldDataSource<int>(field);
            var notifications = 0;
            source.propertyChanged += (_, _) => notifications++;
            source.Dispose();

            field.Value = 1;

            Assert.Zero(notifications);
            Assert.Throws<ObjectDisposedException>(() => source.Value = 2);
        }
    }
}

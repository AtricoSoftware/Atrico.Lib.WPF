using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.TestAttributes.NUnit;
using Atrico.Lib.WPF.PropertyContainer;

namespace Atrico.Lib.WPF.Tests.PropertyContainer
{
    [TestFixture]
    public class TestDependencyPropertyContainerEvents : TestFixtureBase
    {
        private class PropertyHandler
        {
            public int CallCount { get; private set; }
            public PropertyChangedEventArgs Args { get; private set; }
            public object Sender { get; private set; }

            public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                Args = e;
                Sender = sender;
                ++CallCount;
            }
        }

        [Test]
        public void TestSetDefault()
        {
            var name = Guid.NewGuid().ToString();
            var sender = new DependencyObject();

            // Arrange
            var props = new DependencyPropertyContainer(sender);
            var handler = new PropertyHandler();
            props.PropertyChanged += handler.OnPropertyChanged;

            // Act
            props.Set(RandomValues.Integer(), name);
            props.PropertyChanged -= handler.OnPropertyChanged;

            // Assert
            Assert.That(Value.Of(handler.CallCount).Is().EqualTo(1), "Single callback");
            Assert.That(Value.Of(handler.Args.PropertyName).Is().EqualTo(name), "Correct name");
            Assert.That(Value.Of(handler.Sender).Is().ReferenceEqualTo(sender), "Correct sender");
        }

        [Test]
        public void TestSetMultiple()
        {
            var name = Guid.NewGuid().ToString();
            const int count = 10;
            var values = RandomValues.UniqueValues<int>(count);
            var sender = new DependencyObject();

            // Arrange
            var props = new DependencyPropertyContainer(sender);
            var handler = new PropertyHandler();
            props.PropertyChanged += handler.OnPropertyChanged;

            // Act
            foreach (var val in values)
            {
                props.Set(val, name);
            }
            props.PropertyChanged -= handler.OnPropertyChanged;

            // Assert
            Assert.That(Value.Of(handler.CallCount).Is().EqualTo(count), "Multiple callbacks");
            Assert.That(Value.Of(handler.Args.PropertyName).Is().EqualTo(name), "Correct name");
            Assert.That(Value.Of(handler.Sender).Is().ReferenceEqualTo(sender), "Correct sender");
        }

        [Test]
        public void TestSetNoChange()
        {
            var name = Guid.NewGuid().ToString();
            var values = Enumerable.Repeat(123, 10);
            var sender = new DependencyObject();

            // Arrange
            var props = new DependencyPropertyContainer(sender);
            var handler = new PropertyHandler();
            props.PropertyChanged += handler.OnPropertyChanged;

            // Act
            foreach (var val in values)
            {
                props.Set(val, name);
            }
            props.PropertyChanged -= handler.OnPropertyChanged;

            // Assert
            Assert.That(Value.Of(handler.CallCount).Is().EqualTo(1), "Single callback");
            Assert.That(Value.Of(handler.Args.PropertyName).Is().EqualTo(name), "Correct name");
            Assert.That(Value.Of(handler.Sender).Is().ReferenceEqualTo(sender), "Correct sender");
        }
    }
}
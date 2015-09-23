using System;
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
    public class TestDependencyPropertyContainerGetSet<T> : TestPODTypes<T>
    {
        [Test]
        public void TestGetDefault()
        {
            var name = Guid.NewGuid().ToString();
            var expected = default(T);

            // Arrange
            var props = new DependencyPropertyContainer(new DependencyObject());

            // Act
            var value = props.Get<T>(name);

            // Assert
            if (typeof(T) != typeof(string))
            {
                Assert.That(Value.Of(value).Is().EqualTo(expected), "Get default value");
            }
            else
            {
                Assert.That(Value.Of(value as string).Is().Null(), "Get default value (string)");
            }
        }

        [Test]
        public void TestSetAndGet()
        {
            var name = Guid.NewGuid().ToString();
            var expected = RandomValues.Value<T>();

            // Arrange
            var props = new DependencyPropertyContainer(new DependencyObject());
            props.Set(expected, name);

            // Act
            var value = props.Get<T>(name);

            // Assert
            Assert.That(Value.Of(value).Is().EqualTo(expected), "Get value");
        }

        [Test]
        public void TestOverwriteAndGet()
        {
            var name = Guid.NewGuid().ToString();
            var expected = RandomValues.Value<T>();

            // Arrange
            var props = new DependencyPropertyContainer(new DependencyObject());
            props.Set(RandomValues.Value<T>(), name);
            props.Set(expected, name);

            // Act
            var value = props.Get<T>(name);

            // Assert
            Assert.That(Value.Of(value).Is().EqualTo(expected), "Get value");
        }
    }
}
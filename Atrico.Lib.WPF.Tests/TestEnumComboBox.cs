using System;
using System.Threading;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using Atrico.Lib.Assertions;
using Atrico.Lib.Assertions.Constraints;
using Atrico.Lib.Assertions.Elements;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.TestAttributes.NUnit;
using Atrico.Lib.WPF.Controls;

namespace Atrico.Lib.WPF.Tests
{
    [TestFixture, RequiresSTA]
    public class TestEnumComboBox : TestFixtureBase
    {
        private abstract class TestScaffoldWindow<TContent> : IDisposable where TContent : UIElement
        {
            private readonly Window _window;

            public TContent Content { get; private set; }

            protected TestScaffoldWindow(object content)
            {
                _window = new Window {Content = content};
                var windowPeer = new WindowAutomationPeer(_window);
                _window.Show();
                var contentPeer = windowPeer.GetChildren()[0] as UIElementAutomationPeer;
                Content = (contentPeer != null ? contentPeer.Owner as TContent : null);
           }


            #region IDisposable

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~TestScaffoldWindow()
            {
                Dispose(false);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _window.Close();
                }
            }

            #endregion
        }

        private class TestEnumComboBoxWindow : TestScaffoldWindow<ComboBox>
        {
            public TestEnumComboBoxWindow()
                : base(CreateContent())
            {
            }

            private static object CreateContent()
            {
                var control = new EnumComboBox();
                return control;
            }
        }

        private enum TestEnum
        {
            One,
            Two,
            Three,
            Four,
            Five
        }

        [Test]
        public void TestDropdownHasCorrectValues()
        {
            using (var win = new TestEnumComboBoxWindow())
            {
                // Arrange

                // Act
                var items = win.Content.Items;

                // Assert
                Assert.That(Value.Of(items).Count().Is().EqualTo(5), "Correct number of items");
                Assert.That(Value.Of(items[0]).Is().EqualTo(TestEnum.One), "Item1");
                Assert.That(Value.Of(items[1]).Is().EqualTo(TestEnum.Two), "Item2");
                Assert.That(Value.Of(items[2]).Is().EqualTo(TestEnum.Three), "Item3");
                Assert.That(Value.Of(items[3]).Is().EqualTo(TestEnum.Four), "Item4");
                Assert.That(Value.Of(items[4]).Is().EqualTo(TestEnum.Five), "Item5");
            }
        }
    }
}
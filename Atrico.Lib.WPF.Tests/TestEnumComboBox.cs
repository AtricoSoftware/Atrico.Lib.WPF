using System.Diagnostics;
using System.Windows;
using Atrico.Lib.Testing;
using Atrico.Lib.Testing.TestAttributes.NUnit;

namespace Atrico.Lib.Dimensions.Tests
{
    [TestFixture, RequiresSTA]
    public class TestEnumComboBox : TestFixtureBase
    {
        private static Window CreateControlWindow()
        {
            var window = new Window();
            return window;
        }

        [Test]
        public void TestMethod1()
        {
            // Arrange
            var win = CreateControlWindow();

            // Act
            win.Show();
            Debugger.Break();
            win.Close();

            // Assert
        }
    }
}
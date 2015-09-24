using System;
using System.Windows;
using System.Windows.Controls;

namespace Atrico.Lib.WPF.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1;assembly=WpfCustomControlLibrary1"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:EnumComboBox/>
    ///
    /// </summary>
    public class EnumComboBox : ComboBox
    {
        static EnumComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumComboBox), new FrameworkPropertyMetadata(typeof(EnumComboBox)));
        }

        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(
            "EnumType", typeof(Type), typeof(EnumComboBox), new PropertyMetadata(default(Type)));

        public Type EnumType
        {
            get { return (Type) GetValue(EnumTypeProperty); }
            set
            {
                if (value == EnumType)
                {
                    return;
                }
                SetValue(EnumTypeProperty, value);
                PopulateItems(value);
            }
        }

        private void PopulateItems(Type enumType)
        {
            BeginInit();
            Items.Clear();
            foreach (var value in Enum.GetValues(enumType))
            {
                Items.Add(value);
            }
            EndInit();
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using Atrico.Lib.Common.PropertyContainer;
using Atrico.Lib.WPF.PropertyContainer;

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
        private readonly IPropertyContainer _dependencyProperties;

        static EnumComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumComboBox), new FrameworkPropertyMetadata(typeof(EnumComboBox)));
        }

        public EnumComboBox()
        {
            _dependencyProperties = new DependencyPropertyContainer(this);
        }

        public Type EnumType
        {
            get { return _dependencyProperties.Get<Type>(); }
            set
            {
                if (value == EnumType)
                {
                    return;
                }
                _dependencyProperties.Set(value);
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
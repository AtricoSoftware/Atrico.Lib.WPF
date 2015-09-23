﻿using System.Windows;
using Atrico.Lib.Common.PropertyContainer;

namespace Atrico.Lib.WPF.PropertyContainer
{
    public class AttachedPropertyContainer : PropertyContainerBase<DependencyProperty>
    {
        private readonly DependencyObject _owner;

        public AttachedPropertyContainer(DependencyObject owner)
            : base(owner)
        {
            _owner = owner;
        }

        protected override T GetValue<T>(DependencyProperty prop)
        {
            return (T) _owner.GetValue(prop);
        }

        protected override DependencyProperty AmendValue<T>(DependencyProperty prop, T value)
        {
            _owner.SetValue(prop, value);
            return prop;
        }

        protected override DependencyProperty CreateValue<T>(string name, T value)
        {
            var prop = DependencyProperty.RegisterAttached(name, typeof(T), _owner.GetType(), new PropertyMetadata(default(T)));
            return AmendValue(prop, value);
        }
    }
}
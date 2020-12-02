using AspectInjector.Broker;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotation
{
    [Injection(typeof(Notify3Attribute))]
    [Aspect(Scope.PerInstance)]
    [Mixin(typeof(INotifyPropertyChanged))]
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class Notify3Attribute : Attribute, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [Advice(Kind.After, Targets = Target.Public | Target.Setter)]
        public void AfterSetter([Argument(Source.Instance)] object sender, [Argument(Source.Name)] string propertyName)
        {
            this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }

    [Injection(typeof(NotifyAttribute))]
    [Aspect(Scope.PerInstance)]
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class NotifyAttribute : Attribute
    {
    
        [Advice(Kind.After, Targets = Target.Public | Target.Setter)]
        public void AfterSetter([Argument(Source.Instance)] object sender, [Argument(Source.Name)] string propertyName)
        {
            if (sender is ViewModelBase)
            {
                (sender as ViewModelBase).RaisePropertyChanged(propertyName);
            }
        }
    }
}

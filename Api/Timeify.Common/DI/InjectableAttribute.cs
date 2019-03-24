using System;

namespace Timeify.Common.DI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectableAttribute : Attribute
    {
        public enum LifeTimeType
        {
            Hierarchical,
            Container
        }

        public InjectableAttribute() : this(LifeTimeType.Container)
        {
        }

        public InjectableAttribute(LifeTimeType lifetimeManager)
        {
            TargetType = null;
            LifetimeManager = lifetimeManager;
        }

        public InjectableAttribute(Type targetType, LifeTimeType lifetimeManager)
        {
            TargetType = targetType;
            LifetimeManager = lifetimeManager;
        }

        public Type TargetType { get; }

        public LifeTimeType LifetimeManager { get; }

        public override string ToString()
        {
            return $"Target: {TargetType} - LifeTime:{LifetimeManager}";
        }
    }
}
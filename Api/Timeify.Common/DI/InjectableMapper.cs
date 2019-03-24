using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Timeify.Common.DI
{
    public class InjectableMapper
    {
        private Action<Type, Type> hierarchicalRegistrar;
        private bool loadAssemblies;
        private Action<Type, Type> singletonRegistrar;

        public InjectableMapper MapSingleton(Action<Type, Type> registerAction)
        {
            singletonRegistrar = registerAction;
            return this;
        }

        public InjectableMapper MapHierarchical(Action<Type, Type> registerAction)
        {
            hierarchicalRegistrar = registerAction;
            return this;
        }

        /// <summary>
        ///     Needed for Net Core Applications
        /// </summary>
        public InjectableMapper LoadAssemblies()
        {
            loadAssemblies = true;
            return this;
        }

        private void InitAssemblies()
        {
            if (!loadAssemblies) return;
            //foreach (var assemblyName in DependencyContext.Default.GetDefaultAssemblyNames())
            //{
            //    AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);
            //}
        }

        private IDictionary<Type, InjectableAttribute> LoadTypesForInjection()
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies();

            var injectableTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttributes(typeof(InjectableAttribute), true).Any())
                .ToDictionary(
                    key => key,
                    value => (InjectableAttribute) value.GetCustomAttribute(typeof(InjectableAttribute)));

            return injectableTypes;
        }

        public void MapTypes()
        {
            InitAssemblies();
            var injectableTypes = LoadTypesForInjection();

            foreach (var (source, attribute) in injectableTypes)
                switch (attribute.LifetimeManager)
                {
                    case InjectableAttribute.LifeTimeType.Container:
                        singletonRegistrar?.Invoke(attribute.TargetType ?? source, source);
                        break;
                    case InjectableAttribute.LifeTimeType.Hierarchical:
                        hierarchicalRegistrar?.Invoke(attribute.TargetType ?? source, source);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }
    }
}
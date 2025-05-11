using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Infrastructure.ViewController
{
    public static class ControllerUtility
    {
        public static List<Type> GetAllControllerImplementationTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType != null && type.IsSubclassOf(typeof(Controller)))
                .Where(type => type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(Controller<>))
                .ToList();
        }
    }
}
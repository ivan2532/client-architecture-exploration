using System;
using System.Collections.Generic;
using System.Linq;
using Core.Controller;

namespace Core.Utility
{
    public static class ControllerUtility
    {
        public static List<Type> GetAllControllerImplementationTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType != null && type.IsSubclassOf(typeof(ControllerBase)))
                .Where(type => type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(ControllerBase<>))
                .ToList();
        }
    }
}
using System.Collections.Generic;

namespace Subtegral.CommandLine
{
    public static class ReflectionHelpers
    {
        public static System.Type[] GetAllDerivedTypes(this System.AppDomain aAppDomain, System.Type aType)
        {
            var result = new List<System.Type>();
            var assemblies = aAppDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(aType))
                        result.Add(type);
                }
            }
            return result.ToArray();
        }
        public static System.Type[] GetAllDerivedTypes<T>(this System.AppDomain aAppDomain)
        {
            return GetAllDerivedTypes(aAppDomain, typeof(T));
        }
        public static System.Type[] GetTypesWithInterface(this System.AppDomain aAppDomain, System.Type aInterfaceType)
        {
            var result = new List<System.Type>();
            var assemblies = aAppDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (aInterfaceType.IsAssignableFrom(type))
                        result.Add(type);
                }
            }
            return result.ToArray();
        }
        public static System.Type[] GetTypesWithInterface<T>(this System.AppDomain aAppDomain)
        {
            return GetTypesWithInterface(aAppDomain, typeof(T));
        }
    }
}
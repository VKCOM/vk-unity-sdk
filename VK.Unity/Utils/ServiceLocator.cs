using System;
using System.Collections.Generic;

namespace VK.Unity.Utils
{
    internal static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        public static T Resolve<T>()
        {
            Type type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                throw new KeyNotFoundException();
            }

            return (T)_services[type];
        }
    }
}

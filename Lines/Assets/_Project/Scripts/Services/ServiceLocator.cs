using System;
using System.Collections.Generic;

namespace Services
{
    public class ServiceLocator
    {
        private Dictionary<Type, IService> _services = new();

        public static ServiceLocator Instance;

        private ServiceLocator()
        {
        }

        public static void Init()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = new ServiceLocator();
        }

        public void AddService<T>(T serivese) where T : IService
        {
            _services.Add(typeof(T), serivese);
        }


        public T GetService<T>()
        {
            return (T)_services[typeof(T)];
        }

        public void RemoveService<T>(T service) where T : IService
        {
            _services.Remove(typeof(T));
        }
    }
}
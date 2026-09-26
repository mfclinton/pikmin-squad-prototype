using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Systems.Base
{
    public static class ComponentRegistry
    {
        private static readonly Dictionary<Type, IComponentCollection> Collections = new Dictionary<Type, IComponentCollection>();

        public static ComponentCollection<T> GetCollection<T>() where T : Component
        {
            var type = typeof(T);
            if (!Collections.TryGetValue(type, out var collection))
            {
                collection = new ComponentCollection<T>();
                Collections[type] = collection;
            }
            
            return (ComponentCollection<T>)collection;
        }
        
        public static void RegisterComponent<T>(T component) where T : Component
        {
            GetCollection<T>().Register(component);
        }
        
        public static void UnregisterComponent<T>(T component) where T : Component
        {
            GetCollection<T>().Unregister(component);
        }
    }
}
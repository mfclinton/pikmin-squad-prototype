using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Systems.Base
{
    public class ComponentCollection<T> : IComponentCollection where T : Component
    {
        // Internal State
        private readonly HashSet<T> _components = new HashSet<T>();
        public IReadOnlyCollection<T> Components => _components;

        private readonly HashSet<BaseComponentObserver<T>> _observers = new HashSet<BaseComponentObserver<T>>();
        
        #region IComponentCollection Implementation

        public Type ComponentType => typeof(T);
        
        public bool Contains(Component component)
        {
            if (component is T typedComponent)
            {
                return _components.Contains(typedComponent);
            }
            return false;
        }

        public void Clear()
        {
            var componentsToRemove = new List<T>(_components);
            foreach (var component in componentsToRemove)
            {
                Unregister(component);
            }
        }
        
        #endregion

        #region Registration Methods

        public void Register(T component)
        {
            if (_components.Add(component))
            {
                foreach (var observer in _observers)
                {
                    observer.ComponentEnabled(component);
                }
            }
        }
        
        public void Unregister(T component)
        {
            if (_components.Remove(component))
            {
                foreach (var observer in _observers)
                {
                    observer.ComponentDisabled(component);
                }
            }
        }
        
        public void AddObserver(BaseComponentObserver<T> observer)
        {
            if (_observers.Add(observer))
            {
                foreach (var component in _components)
                {
                    observer.ComponentEnabled(component);
                }
            }
        }
        
        public void RemoveObserver(BaseComponentObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        #endregion
    }
}
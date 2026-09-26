using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Systems.Base
{
    public class BaseComponentObserver<T> where T : Component
    {
        public IReadOnlyCollection<T> Components => ComponentRegistry.GetCollection<T>().Components;
        
        public void RegisterObserver()
        {
            ComponentRegistry.GetCollection<T>().AddObserver(this);
        }
        
        public void UnregisterObserver()
        {
            ComponentRegistry.GetCollection<T>().RemoveObserver(this);
        }
        
        public virtual void ComponentEnabled(T component) {}
        
        public virtual void ComponentDisabled(T component) {}
    }
}
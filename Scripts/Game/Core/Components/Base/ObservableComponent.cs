using Game.Core.Systems.Base;
using UnityEngine;

namespace Game.Core.Components.Base
{
    public abstract class ObservableComponent<T> : MonoBehaviour where T : Component
    {
        protected virtual void OnEnable()
        {
            ComponentRegistry.RegisterComponent(this as T);
            OnComponentEnabled();
        }
        
        protected virtual void OnDisable()
        {
            ComponentRegistry.UnregisterComponent(this as T);
            OnComponentDisabled();
        }
        
        protected virtual void OnComponentEnabled() { }
        
        protected virtual void OnComponentDisabled() { }
    }
}
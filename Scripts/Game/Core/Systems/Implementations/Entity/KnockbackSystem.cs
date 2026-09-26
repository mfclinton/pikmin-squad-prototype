using Game.Core.Components.Implementations.Entity;
using Game.Core.Systems.Base;
using UnityEngine;

namespace Game.Core.Systems.Implementations.Entity
{
    public class KnockbackSystem : MonoBehaviour
    {
        // Component Observers
        private readonly PendingUpdateComponentObserver<KnockbackComponent> _knockbackComponents = 
            new PendingUpdateComponentObserver<KnockbackComponent>();
        
        private void OnEnable()
        {
            // Register Observers
            _knockbackComponents.RegisterObserver();
        }

        private void OnDisable()
        {
            // Unregister Observers
            _knockbackComponents.UnregisterObserver();
        }
        
        private void FixedUpdate()
        {
            foreach (var knockbackComponent in _knockbackComponents.ComponentsWithPendingUpdates)
            {
                // Process Knockback
                knockbackComponent.ProcessKnockback();
            }
        }
    }
}
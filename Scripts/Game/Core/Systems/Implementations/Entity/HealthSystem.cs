using Game.Core.Components.Implementations.Entity;
using Game.Core.Systems.Base;
using UnityEngine;

namespace Game.Core.Systems.Implementations.Entity
{
    public class HealthSystem : MonoBehaviour
    {
        // Component Observers
        private readonly PendingUpdateComponentObserver<HealthComponent> _healthComponents = new PendingUpdateComponentObserver<HealthComponent>();
        
        private void OnEnable()
        {
            // Register Observers
            _healthComponents.RegisterObserver();
        }

        private void OnDisable()
        {
            // Unregister Observers
            _healthComponents.UnregisterObserver();
        }
        
        private void FixedUpdate()
        {
            foreach (var healthComponent in _healthComponents.ComponentsWithPendingUpdates)
            {
                // Process Health Change
                healthComponent.ProcessHealthChange();
            }
        }
    }
}
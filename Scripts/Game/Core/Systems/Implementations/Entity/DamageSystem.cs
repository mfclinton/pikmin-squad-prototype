using Game.Core.Components.Implementations.Entity;
using Game.Core.Systems.Base;
using UnityEngine;

namespace Game.Core.Systems.Implementations.Entity
{
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] public LayerMask TargetLayerMask;
        
        // Component Observers
        private readonly BaseComponentObserver<DamageSourceComponent> _damageSources = 
            new BaseComponentObserver<DamageSourceComponent>();
        
        // Collision buffer
        private Collider2D[] _collisionResultsBuffer;
        private ContactFilter2D _collisionFilter;
        
        private void Awake()
        {
            // Initialize Collision Data
            _collisionResultsBuffer = new Collider2D[10];
            _collisionFilter = new ContactFilter2D()
            {
                layerMask = TargetLayerMask,
                useLayerMask = true,
            };
        }

        private void OnEnable()
        {
            // Register Observers
            _damageSources.RegisterObserver();
        }

        private void OnDisable()
        {
            // Unregister Observers
            _damageSources.UnregisterObserver();
        }
        
        private void FixedUpdate()
        {
            foreach (var damageSource in _damageSources.Components)
            {
                CheckForCollision(damageSource);
            }
        }
        
        private void CheckForCollision(DamageSourceComponent damageSource)
        {
            int count = damageSource.Collider2D.Overlap(_collisionFilter, _collisionResultsBuffer);
            for (int i = 0; i < count; i++)
            {
                Collider2D target = _collisionResultsBuffer[i];
                if (!target)
                    continue;

                // Handle Health
                if (target.TryGetComponent(out HealthComponent healthComponent))
                {
                    if (healthComponent.IsInvincible())
                        continue;
                    
                    // Queue damage
                    healthComponent.QueueHealthChange(-damageSource.Damage);
                }
                
                // Handle Knockback
                if (target.TryGetComponent(out KnockbackComponent knockbackComponent))
                {
                    Vector2 knockbackDirection = ((Vector2)target.transform.position - 
                        (Vector2)damageSource.transform.position).normalized;
                    
                    knockbackComponent.QueueKnockback(knockbackDirection * damageSource.KnockbackForce);
                }
            }
        }
    }
}
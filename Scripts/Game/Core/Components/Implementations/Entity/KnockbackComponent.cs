using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class KnockbackComponent : PendingUpdateComponent<KnockbackComponent>
    {
        [SerializeField] public float KnockbackResistance = 1f;
        
        // References
        [HideInInspector] public Rigidbody2D Rigidbody;
        
        // Pending Update State
        public Vector2 PendingKnockbackForce = Vector2.zero;
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
    
    // Extensions
    public static class KnockbackComponentExtensions
    {
        public static void QueueKnockback(this KnockbackComponent knockbackComponent, Vector2 force)
        {
            knockbackComponent.PendingKnockbackForce += force;
            knockbackComponent.SetPendingUpdate();
        }

        public static void ProcessKnockback(this KnockbackComponent knockbackComponent)
        {
            if (knockbackComponent.PendingKnockbackForce.sqrMagnitude > 0)
            {
                // Apply resistance
                Vector2 adjustedForce = knockbackComponent.PendingKnockbackForce / knockbackComponent.KnockbackResistance;
                knockbackComponent.Rigidbody.AddForce(adjustedForce, ForceMode2D.Impulse);
            }
            
            knockbackComponent.ResetKnockback();
        }
        
        public static void ResetKnockback(this KnockbackComponent knockbackComponent)
        {
            knockbackComponent.PendingKnockbackForce = Vector2.zero;
            knockbackComponent.ClearPendingUpdate();
        }
    }
}
using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.Entity
{
    public abstract class AttackComponent : ObservableComponent<AttackComponent>
    {
        [Header("Attack Settings")]
        [SerializeField] public float AttackRange = 2f;
        [SerializeField] public float AttackDamage = 10f;
        [SerializeField] public float AttackCooldown = 1f;
        
        // State
        [HideInInspector] public float LastAttackTime = -1000f;
    }

    // Extensions
    public static class AttackComponentExtensions
    {
        public static bool CanAttack(this AttackComponent attackComponent, Transform target)
        {
            return target && !attackComponent.IsOnCooldown() && attackComponent.IsInRange(target);
        }

        // Helpers
        public static bool IsOnCooldown(this AttackComponent attackComponent)
        {
            return Time.time - attackComponent.LastAttackTime < attackComponent.AttackCooldown;
        }

        public static bool IsInRange(this AttackComponent attackComponent, Transform target)
        {
            return target && attackComponent.IsInRange(target);
        }
    }
}
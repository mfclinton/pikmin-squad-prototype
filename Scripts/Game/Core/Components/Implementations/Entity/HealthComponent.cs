using System;
using Game.Core.Components.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.Components.Implementations.Entity
{
    public class HealthComponent : PendingUpdateComponent<HealthComponent>
    {
        [SerializeField] public int MaxHealth = 100;
        [SerializeField] public float InvincibilityTimeAfterDamaged = 1f;
        
        // Internal State
        public int CurrentHealth;
        public float TimeLastDamaged = 0f;
        
        // Pending Update State
        public int PendingHealthChange = 0;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }
    }
    
    // Extensions
    public static class HealthComponentExtensions
    {
        public static void QueueHealthChange(this HealthComponent healthComponent, int delta)
        {
            healthComponent.PendingHealthChange += delta;
            healthComponent.TimeLastDamaged = Time.time;
            healthComponent.SetPendingUpdate();
        }

        public static void ProcessHealthChange(this HealthComponent healthComponent)
        {
            healthComponent.CurrentHealth += healthComponent.PendingHealthChange;
            healthComponent.CurrentHealth = Mathf.Clamp(healthComponent.CurrentHealth, 0, healthComponent.MaxHealth);
            healthComponent.ResetHealthChange();
        }
        
        public static void ResetHealthChange(this HealthComponent healthComponent)
        {
            healthComponent.PendingHealthChange = 0;
            healthComponent.ClearPendingUpdate();
        }

        public static bool IsInvincible(this HealthComponent healthComponent)
        {
            return healthComponent.TimeLastDamaged + healthComponent.InvincibilityTimeAfterDamaged > Time.time;
        }
    }
}
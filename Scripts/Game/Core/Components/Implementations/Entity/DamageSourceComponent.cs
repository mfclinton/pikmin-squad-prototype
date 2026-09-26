using System;
using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.Entity
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageSourceComponent : ObservableComponent<DamageSourceComponent>
    {
        [Header("Damage Settings")]
        public int Damage = 10;
        public float KnockbackForce = 500f;
        
        // References
        [HideInInspector] public Collider2D Collider2D;
        
        private void Awake()
        {
            Collider2D = GetComponent<Collider2D>();
        }
    }
}
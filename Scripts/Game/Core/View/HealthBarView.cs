using System;
using Game.Core.Components.Implementations;
using Game.Core.Components.Implementations.Entity;
using UnityEngine;

namespace Game.Core.View
{
    public class HealthBarView : MonoBehaviour
    {
        // References
        [HideInInspector] private SpriteRenderer Renderer;
        public HealthComponent HealthComponent;

        // Internal State
        private MaterialPropertyBlock _propertyBlock;

        private void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
            
            _propertyBlock = new MaterialPropertyBlock();
        }

        private void Update()
        {
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if (!HealthComponent || !Renderer)
                return;

            float normalized = Mathf.Clamp01((float)HealthComponent.CurrentHealth / HealthComponent.MaxHealth);
            _propertyBlock.SetFloat("_T", normalized);
            
            Renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}
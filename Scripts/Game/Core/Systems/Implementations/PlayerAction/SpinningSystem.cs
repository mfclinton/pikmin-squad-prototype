using Game.Core.Components.Implementations.Entity;
using Game.Core.Components.Implementations.PlayerAction;
using Game.Player.Controls.Data;
using Game.Player.Controls.Input;
using UnityEngine;

namespace Game.Core.Systems.Implementations.PlayerAction
{
    public class SpinningSystem : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] private LayerMask _spinnableLayerMask;
        
        [Header("Spin Settings")]
        [SerializeField] private float _minScrollDelta = 0.1f;
        
        // Internal State
        private float _scrollDelta = 0f;
        
        private void Update()
        {
            var inputContext = InputManager.Instance.CurrentInputContext;
            
            UpdateSpinInput(inputContext);
        }
        
        private void FixedUpdate()
        {
            // Check if Scroll Action is Triggered
            if (Mathf.Abs(_scrollDelta) < _minScrollDelta)
                return;

            // Get Selected Entity
            EntityComponent selectedEntity = EntityComponentSelector.Instance.SelectionContext.SelectedComponent;
            if (!selectedEntity)
                return;

            // Get Seletected Spinnable Component
            SpinnableComponent spinnable = selectedEntity.GetComponent<SpinnableComponent>();
            if (!spinnable)
                return;
                
            // Process Input
            ApplySpin(spinnable, _scrollDelta);
        }

        private void UpdateSpinInput(InputContext inputContext)
        {
            _scrollDelta += inputContext.ScrollDelta.y;
        }

        private void ApplySpin(SpinnableComponent spinnable, float scrollDelta)
        {
            float spinForce = scrollDelta * spinnable.SpinForceMultiplier;
            spinnable.Rigidbody2D.AddTorque(spinForce);
            _scrollDelta = 0f;
        }
    }
}
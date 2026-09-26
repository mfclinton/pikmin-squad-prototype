using Game.Core.Components.Implementations.Entity;
using Game.Core.Components.Implementations.PlayerAction;
using Game.Player.Controls.Input;
using Game.Player.Controls.Data;
using Game.Utilities;
using UnityEngine;

namespace Game.Core.Systems.Implementations.PlayerAction
{
    public class ThrowingSystem : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] private LayerMask _selectableLayerMask;
        
        [Header("Input Settings")]
        [SerializeField] private bool _toggleInput = true;
        
        private void FixedUpdate()
        {
            // Get Input Context
            var inputContext = InputManager.Instance.CurrentInputContext;
            
            // Get Selected Entity
            EntityComponent selectedEntity = EntityComponentSelector.Instance.SelectionContext.SelectedComponent;
            if (!selectedEntity)
                return;

            // Get Seletected Throwable Component
            ThrowableComponent throwable = selectedEntity.GetComponent<ThrowableComponent>();
            if (!throwable)
                return;
                
            // Process Input
            HandleDragging(throwable, inputContext);
        }
        
        private void HandleDragging(ThrowableComponent throwable, InputContext inputContext)
        {
            ApplyForce(throwable, inputContext.PointerWorldPosition);
        }

        private void ApplyForce(ThrowableComponent throwable, Vector2 targetPosition)
        {
            PhysicsUtils.ApplySpringForce(
                throwable.Rigidbody2D,
                throwable.transform.position,
                targetPosition,
                throwable.MovementSpeed,
                throwable.MovementDamping
            );
        }
    }
}
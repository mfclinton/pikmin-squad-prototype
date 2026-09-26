using Game.Core.Components.Implementations.Entity;
using Game.Core.Components.Implementations.PlayerAction;
using Game.Player.Controls.Data;
using Game.Player.Controls.Input;
using Game.Utilities;
using UnityEngine;

namespace Game.Core.Systems.Implementations.PlayerAction
{
    public class RecallSystem : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] private LayerMask _recallableLayerMask;
        
        [Header("Recall Settings")]
        [SerializeField] private float _maxRecallDistance = 15f;
        
        private void FixedUpdate()
        {         
            // Get Input Context
            var inputContext = InputManager.Instance.CurrentInputContext;

            // Check if Recall Action is Triggered
            if (!inputContext.IsRightMouseHeld)
                return;

            // Get Selected Entity
            EntityComponent selectedEntity = EntityComponentSelector.Instance.SelectionContext.LastSelectedComponent;
            Debug.Log($"Selected Entity: {selectedEntity}");
            if (!selectedEntity)
                return;

            // Get Seletected Recallable Component
            RecallableComponent recallable = selectedEntity.GetComponent<RecallableComponent>();
            if (!recallable)
                return;
                
            // Process Input
            HandleRecall(recallable, inputContext);
        }
        
        private void HandleRecall(RecallableComponent recallable, InputContext inputContext)
        {
            Vector2 cursorPosition = inputContext.PointerWorldPosition;
            Vector2 objectPosition = recallable.transform.position;
            
            float distance = Vector2.Distance(cursorPosition, objectPosition);
            if (distance > _maxRecallDistance)
                return;
            
            ApplyRecallForce(recallable, cursorPosition);
        }
        
        private void ApplyRecallForce(RecallableComponent recallable, Vector2 targetPosition)
        {
            PhysicsUtils.ApplySpringForce(
                recallable.Rigidbody2D,
                recallable.transform.position,
                targetPosition,
                recallable.MovementSpeed,
                recallable.MovementDamping
            );
        }
    }
}
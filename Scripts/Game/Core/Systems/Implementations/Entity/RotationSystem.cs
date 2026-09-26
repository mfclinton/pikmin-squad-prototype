using Game.Core.Components.Implementations.Entity;
using Game.Core.Systems.Base;
using Game.Utilities;
using UnityEngine;

namespace Game.Core.Systems.Implementations.Entity
{
    public class RotationSystem : MonoBehaviour
    {
        // Component Observers
        private readonly BaseComponentObserver<RotationComponent> _rotations = new BaseComponentObserver<RotationComponent>();
        
        private void OnEnable()
        {
            // Register Observers
            _rotations.RegisterObserver();
        }
        
        private void OnDisable()
        {
            // Unregister Observers
            _rotations.UnregisterObserver();
        }
        
        private void FixedUpdate()
        {
            UpdateRotations();
        }

        private void UpdateRotations()
        {
            foreach (var rotationComponent in _rotations.Components)
            {
                UpdateRotation(rotationComponent);
            }
        }

        private void UpdateRotation(RotationComponent rotationComponent)
        {
            Vector2? targetPosition = null;
            
            // Target
            if (rotationComponent.TargetingComponent && rotationComponent.TargetingComponent.CurrentTarget)
            {
                targetPosition = rotationComponent.TargetingComponent.CurrentTarget.position;
            }
            
            // Rotate
            if (targetPosition.HasValue)
            {
                Vector2 currentPosition = rotationComponent.Rigidbody2D.position;
                Vector2 direction = (targetPosition.Value - currentPosition).normalized;
                
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationComponent.RotationOffset;
                float currentAngle = rotationComponent.transform.eulerAngles.z;
                
                PhysicsUtils.ApplyRotationalSpringForce(
                    rotationComponent.Rigidbody2D,
                    currentAngle,
                    targetAngle,
                    rotationComponent.RotationSpeed,
                    rotationComponent.RotationDamping);
            }
        }
    }
}
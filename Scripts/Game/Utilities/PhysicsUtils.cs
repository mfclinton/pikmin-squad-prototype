using UnityEngine;

namespace Game.Utilities
{
    public static class PhysicsUtils
    {
        #region Spring Force

        public static void ApplySpringForce(
            Rigidbody2D rigidbody, 
            Vector2 currentPosition, 
            Vector2 targetPosition, 
            float springStrength, 
            float damping,
            ForceMode2D forceMode = ForceMode2D.Force)
        {
            Vector2 displacement = targetPosition - currentPosition;
            Vector2 springForce = springStrength * displacement;
            Vector2 dampingForce = -damping * rigidbody.linearVelocity;
            Vector2 totalForce = springForce + dampingForce;
            
            rigidbody.AddForce(totalForce, forceMode);
        }

        public static void ApplyRotationalSpringForce(
            Rigidbody2D rigidbody, 
            float currentRotation, 
            float targetRotation, 
            float rotationSpeed, 
            float rotationalDamping)
        {
            // Calculate the shortest angle between current and target rotation
            float angleDifference = Mathf.DeltaAngle(currentRotation, targetRotation);
            
            // Spring torque
            float springTorque = rotationSpeed * angleDifference;
            
            // Damping torque
            float dampingTorque = -rotationalDamping * rigidbody.angularVelocity;
            
            // Apply combined torque
            rigidbody.AddTorque(springTorque + dampingTorque);
        }

        #endregion
    }
}
using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.Entity
{
    public class LineOfSightComponent : ObservableComponent<LineOfSightComponent>
    {
        // Settings
        [SerializeField] public LayerMask ObstacleLayerMask;
        [SerializeField] public float ViewDistance = 10f;
        [SerializeField] public float MemoryDuration = 3f;
        
        // Memory State
        [HideInInspector] public float TimeTargetLastSeen;
    }

    // Extensions
    public static class LineOfSightExtensions
    {
        public static bool HasLineOfSight(this LineOfSightComponent lineOfSight, Transform target)
        {
            if (!target)
                return false;
            
            Vector2 direction = target.position - lineOfSight.transform.position;
            
            // Check View Distance
            float distance = direction.magnitude;
            if (distance > lineOfSight.ViewDistance)
                return false;
            
            // Check For Obstacles
            RaycastHit2D hit = Physics2D.Raycast(
                lineOfSight.transform.position,
                direction.normalized,
                distance,
                lineOfSight.ObstacleLayerMask
            );
                        
            return !hit.collider;
        }
        
        public static bool HasTargetMemory(this LineOfSightComponent lineOfSight)
        {
            if (lineOfSight.TimeTargetLastSeen <= 0)
                return false;
                
            float timeSinceLastSeen = Time.time - lineOfSight.TimeTargetLastSeen;
            return timeSinceLastSeen <= lineOfSight.MemoryDuration;
        }
        
        public static void UpdateTargetVisibility(this LineOfSightComponent lineOfSight, Transform target, bool isVisible)
        {
            if (isVisible && target)
            {
                lineOfSight.TimeTargetLastSeen = Time.time;
            }
        }
    }
}
using System;
using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.Entity
{
    public class TargetingComponent : ObservableComponent<TargetingComponent>
    {
        // Settings
        [SerializeField] public TargetType TargetsType;
        
        // References
        [SerializeField] public LineOfSightComponent LineOfSight;
        
        // State
        [HideInInspector] public Transform CurrentTarget;

        private void Awake()
        {
            LineOfSight = GetComponent<LineOfSightComponent>();
        }
    }

    // Extensions
    public static class TargetingComponentExtensions
    {
        public static void UpdateTarget(this TargetingComponent targeting, Transform newTarget)
        {
            targeting.CurrentTarget = newTarget;
            if (newTarget && targeting.LineOfSight)
            {
                targeting.LineOfSight.UpdateTargetVisibility(newTarget, true);
            }
        }
    }
}
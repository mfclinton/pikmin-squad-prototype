using System.Linq;
using Game.Core.Components.Implementations.Entity;
using Game.Core.Systems.Base;
using UnityEngine;

namespace Game.Core.Systems.Implementations.Entity
{
    public class TargetingSystem : MonoBehaviour
    {
        [SerializeField] private float _updateRate = 0.5f;
        
        // Component Observers
        private readonly BaseComponentObserver<TargetingComponent> _targeters = new BaseComponentObserver<TargetingComponent>();
        private readonly BaseComponentObserver<TargetableComponent> _targetables = new BaseComponentObserver<TargetableComponent>();
        
        // Internal State
        private float _timeUntilNextUpdate;
        
        private void OnEnable()
        {
            // Register Observers
            _targeters.RegisterObserver();
            _targetables.RegisterObserver();
        }
        
        private void OnDisable()
        {
            // Unregister Observers
            _targeters.UnregisterObserver();
            _targetables.UnregisterObserver();
        }
        
        private void FixedUpdate()
        {
            UpdateAllTargets();
        }

        private void UpdateAllTargets()
        {
            _timeUntilNextUpdate -= Time.deltaTime;
            if (_timeUntilNextUpdate <= 0f)
            {
                foreach (var targeter in _targeters.Components)
                {
                    if (!targeter.LineOfSight)
                        continue;
                        
                    // Check Line of Sight
                    CheckCurrentTargetVisibility(targeter);
                    
                    // Update Target
                    if (!targeter.CurrentTarget || !targeter.LineOfSight.HasTargetMemory())
                    {
                        UpdateNewTarget(targeter);
                        Debug.Log($"Targeter {targeter.name} updated target to {targeter?.CurrentTarget}");
                    }
                }

                _timeUntilNextUpdate = _updateRate;
            }
        }
        
        private void CheckCurrentTargetVisibility(TargetingComponent targeter)
        {
            if (!targeter.CurrentTarget)
                return;
            
            bool isVisible = targeter.LineOfSight.HasLineOfSight(targeter.CurrentTarget);
            targeter.LineOfSight.UpdateTargetVisibility(targeter.CurrentTarget, isVisible);
        }
        
        private void UpdateNewTarget(TargetingComponent targeter)
        {
            // Nearby Targetables
            var validTargets = _targetables.Components
                .Where(t => t.Type == targeter.TargetsType);

            // Filter by Line of Sight
            validTargets = validTargets
                .Where(t => targeter.LineOfSight.HasLineOfSight(t.transform));
            
            // Sort by Distance
            validTargets = validTargets.OrderBy(t => 
                Vector3.Distance(t.transform.position, targeter.transform.position));

            // Set Target
            var newTarget = validTargets.FirstOrDefault()?.transform;
            targeter.UpdateTarget(newTarget);
        }
    }
}
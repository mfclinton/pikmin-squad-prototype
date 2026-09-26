using Game.Core.Components.Implementations.Entity;
using Game.Core.Systems.Base;
using UnityEngine;

namespace Game.Core.Systems.Implementations.Entity
{
    public class NavigationSystem : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _updateRate = 0.5f;
        
        // Component Observers
        private readonly BaseComponentObserver<NavigationComponent> _navigation = new BaseComponentObserver<NavigationComponent>();
        
        private float _timeUntilNextPathUpdate;
        
        private void OnEnable()
        {
            // Register Observers
            _navigation.RegisterObserver();
        }

        private void OnDisable()
        {
            // Unregister Observers
            _navigation.UnregisterObserver();
        }

        private void FixedUpdate()
        {
            UpdateTargetPosition();
            UpdatePaths();
            MoveAlongPaths();
        }

        #region Update Loops

        private void UpdateTargetPosition()
        {
            foreach (var navigationComponent in _navigation.Components)
            {
                if (navigationComponent.UseTargetablePosition && navigationComponent.TargetingComponent != null && navigationComponent.TargetingComponent.CurrentTarget != null)
                {
                    navigationComponent.TargetPosition = navigationComponent.TargetingComponent.CurrentTarget.position;
                }
                else
                {
                    navigationComponent.TargetPosition = navigationComponent.Rigidbody2D.position;
                }
            }
        }
        
        private void UpdatePaths()
        {
            _timeUntilNextPathUpdate -= Time.deltaTime;
            if (_timeUntilNextPathUpdate <= 0f)
            {
                foreach (var navigationComponent in _navigation.Components)
                    UpdatePath(navigationComponent, navigationComponent.TargetPosition);
                
                _timeUntilNextPathUpdate = _updateRate;
            }
        }
        
        private void MoveAlongPaths()
        {
            foreach (var navigationComponent in _navigation.Components)
            {
                MoveAlongPath(navigationComponent);
            }
        }

        #endregion

        #region PathFinding

        private void UpdatePath(NavigationComponent navigationComponent, Vector2 targetPosition)
        {
            if (navigationComponent.ReachedEndOfPath)
                return;

            navigationComponent.Seeker.StartPath(navigationComponent.Rigidbody2D.position, targetPosition, navigationComponent.OnPathUpdated);
        }

        private void MoveAlongPath(NavigationComponent navigationComponent)
        {
            if (navigationComponent.ReachedEndOfPath || navigationComponent.CurrentWaypointIndex >= navigationComponent.Path.Count)
                return;
            
            Vector2 currentPosition = navigationComponent.Rigidbody2D.position;
            
            // Reached Waypoint
            float distance = Vector2.Distance(currentPosition, navigationComponent.CurrentWaypoint);
            if (distance < navigationComponent.NextWaypointDistance)
            {
                navigationComponent.CurrentWaypointIndex++;
                if (navigationComponent.CurrentWaypointIndex >= navigationComponent.Path.Count)
                {
                    if (navigationComponent.EndReachableEnabled)
                        navigationComponent.ReachedEndOfPath = true;
                    
                    return;
                }
            }

            // Move
            Vector2 direction = (navigationComponent.CurrentWaypoint - currentPosition).normalized;
            navigationComponent.Rigidbody2D.AddForce(direction * navigationComponent.Speed, ForceMode2D.Force);
        }

        #endregion
    }
}
using System.Collections.Generic;
using Game.Core.Components.Base;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.Components.Implementations.Entity
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Seeker))]
    public class NavigationComponent : ObservableComponent<NavigationComponent>
    {
        // Settings
        [Header("Navigation Settings")]
        [SerializeField] public float Speed = 5f;
        [SerializeField] public float NextWaypointDistance = 0.1f;
        [SerializeField] public bool EndReachableEnabled = true;
        [SerializeField] public bool UseTargetablePosition = true;
        
        // Internal State
        public Vector2 TargetPosition;
        public List<Vector3> Path = new List<Vector3>();  
        public int CurrentWaypointIndex = 0;
        public bool ReachedEndOfPath = false;
        
        // References
        [HideInInspector] public Rigidbody2D Rigidbody2D;
        [HideInInspector] public Seeker Seeker;
        [HideInInspector] public TargetingComponent TargetingComponent;
        
        // Getters
        public Vector2 CurrentWaypoint => Path[CurrentWaypointIndex];
        
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Seeker = GetComponent<Seeker>();
            TargetingComponent = GetComponent<TargetingComponent>();
        }
        
        public void OnPathUpdated(Path path)
        {
            if (path.error)
            {
                Debug.LogError($"Pathfinding error: {path.errorLog}");
                return;
            }
            
            Path = path.vectorPath;
            CurrentWaypointIndex = 0;
        }
        
        
        #region Gizmos

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetPosition, 0.2f);
        }

        #endregion
    }
}
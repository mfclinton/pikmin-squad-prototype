using Game.Core.Components.Base;
using Game.Core.Components.Implementations.Entity;
using UnityEngine;

namespace Game.Core.Components.Implementations.Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RotationComponent : ObservableComponent<RotationComponent>
    {
        [Header("Rotation Settings")]
        [SerializeField] public float RotationSpeed = 5f;
        [SerializeField] public float RotationDamping = 0.5f;
        [SerializeField] public float RotationOffset = 0f;
        
        // References
        [HideInInspector] public Rigidbody2D Rigidbody2D;
        [HideInInspector] public TargetingComponent TargetingComponent;
        
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            TargetingComponent = GetComponent<TargetingComponent>();
        }
    }
}
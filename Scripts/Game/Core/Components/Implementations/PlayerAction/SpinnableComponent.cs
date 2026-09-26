using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.PlayerAction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpinnableComponent : ObservableComponent<SpinnableComponent>
    {
        [SerializeField] public float SpinForceMultiplier = 100f;
        
        // References
        [HideInInspector] public Rigidbody2D Rigidbody2D;
        
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}
using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.PlayerAction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RecallableComponent : ObservableComponent<RecallableComponent>
    {
        [Header("Spring Settings")]
        [SerializeField] public float MovementSpeed = 1000f;
        [SerializeField] public float MovementDamping = 200f;
        
        [HideInInspector] public Rigidbody2D Rigidbody2D;
        
        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}
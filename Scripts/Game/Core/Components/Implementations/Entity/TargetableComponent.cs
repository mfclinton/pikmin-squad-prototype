using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Components.Implementations.Entity
{
    public enum TargetType
    {
        Player,
        Enemy,
        Neutral
    }

    public class TargetableComponent : ObservableComponent<TargetableComponent>
    {
        [SerializeField] public TargetType Type;
    }
}
using System;
using UnityEngine;

namespace Game.Core.Systems.Base
{
    public interface IComponentCollection
    {
        Type ComponentType { get; }
                
        bool Contains(Component component);
        
        void Clear();
    }
}
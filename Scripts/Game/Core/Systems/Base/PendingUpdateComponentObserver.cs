using System.Collections.Generic;
using Game.Core.Components.Base;
using UnityEngine;

namespace Game.Core.Systems.Base
{
    public class PendingUpdateComponentObserver<T> : BaseComponentObserver<T> where T : PendingUpdateComponent<T>
    {
        public IEnumerable<T> ComponentsWithPendingUpdates
        {
            get
            {
                foreach (var component in Components)
                {
                    if (!component.HasPendingUpdate)
                        continue;
                    
                    yield return component;
                }
            }
        }
    }
}
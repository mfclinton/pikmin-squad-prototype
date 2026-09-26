using UnityEngine;

namespace Game.Core.Components.Base
{
    public abstract class PendingUpdateComponent<T> : ObservableComponent<T> where T : PendingUpdateComponent<T>
    {
        [HideInInspector] public bool HasPendingUpdate = false;
    }
    
    // Extensions
    public static class PendingUpdateComponentExtensions
    {
        public static void SetPendingUpdate<T>(this T component) where T : PendingUpdateComponent<T>
        {
            component.HasPendingUpdate = true;
        }

        public static void ClearPendingUpdate<T>(this T component) where T : PendingUpdateComponent<T>
        {
            component.HasPendingUpdate = false;
        }
    }
}
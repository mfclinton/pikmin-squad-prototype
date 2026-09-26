using UnityEngine;
using Game.Player.Controls.Data;

namespace Game.Utilities
{
    public static class RaycastUtils
    {
        public static bool TryRaycastComponent<T>(InputContext inputContext, LayerMask layerMask, out T component) where T : Component
        {
            component = null;
            
            RaycastHit2D hit = Physics2D.Raycast(
                inputContext.ScreenPointRay.origin, 
                inputContext.ScreenPointRay.direction, 
                Mathf.Infinity, 
                layerMask);
                
            if (!hit.collider)
                return false;
                
            component = hit.collider.GetComponent<T>();
            return component != null;
        }
    }
}
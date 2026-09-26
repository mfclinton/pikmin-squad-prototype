using Game.Player.Controls.Data;
using Game.Utilities;
using UnityEngine;

namespace Game.Player.Controls.Input
{
    [DefaultExecutionOrder(-1)]
    public class ComponentSelectorService<T> : MonoBehaviour where T : Component
    {
        [Header("Selection Settings")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private bool _toggleSelection = false;
        
        // State
        public SelectionContext<T> SelectionContext { get; private set; } = new SelectionContext<T>();
        
        protected virtual void Update()
        {
            var inputContext = InputManager.Instance.CurrentInputContext;
            
            // Update Hover State
            UpdateHovered(inputContext);
            
            // Update Selected State
            UpdateSelected(inputContext);
        }
        
        protected void UpdateHovered(InputContext inputContext)
        {
            if (RaycastUtils.TryRaycastComponent<T>(inputContext, _layerMask, out T component))
            {
                // Hovered
                SelectionContext.HoveredComponent = component;
            }
            else
            {
                // Not Hovered
                SelectionContext.HoveredComponent = null;
            }
        }
        
        protected void UpdateSelected(InputContext inputContext)
        {
            // Case 1: Unselect on Mouse Release
            if (inputContext.LeftMouseReleased && !_toggleSelection)
            {
                SetSelected(null);
                return;
            }
            
            // No Mouse Pressed
            if (!inputContext.LeftMousePressed)
                return;
                
            // Case 2: No Component Hovered
            if (!SelectionContext.HoveredComponent)
            {
                if (!_toggleSelection)
                {
                    SetSelected(null);
                }
                return;
            }
            
            // Case 3: Handle Toggle Selection
            bool isClickingSelectedComponent = SelectionContext.HoveredComponent == SelectionContext.SelectedComponent;
            if (_toggleSelection && isClickingSelectedComponent)
            {
                SetSelected(null);
                return;
            }
            
            // Case 4: Select New Component
            SetSelected(SelectionContext.HoveredComponent);
        }

        private void SetSelected(T component)
        {
            SelectionContext.LastSelectedComponent = SelectionContext.SelectedComponent;
            SelectionContext.SelectedComponent = component;
        }
    }
}
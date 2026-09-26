using UnityEngine;

namespace Game.Player.Controls.Data
{
    public class SelectionContext<T> where T : Component
    {
        public T HoveredComponent { get; set; }
        
        public T SelectedComponent { get; set; }
        
        public T LastSelectedComponent { get; set; }
    }
}
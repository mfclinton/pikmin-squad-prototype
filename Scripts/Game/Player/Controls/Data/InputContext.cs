using UnityEngine;

namespace Game.Player.Controls.Data
{
    public class InputContext
    {
        // Position Inputs
        public Vector2 PointerPosition { get; set; }
        public Vector2 PointerDelta { get; set; }
        public Vector2 PointerWorldPosition { get; set; }
        
        // Button States
        public bool LeftMousePressed { get; set; }
        public bool LeftMouseReleased { get; set; }
        public bool IsLeftMouseHeld { get; set; }
        
        public bool RightMousePressed { get; set; }
        public bool RightMouseReleased { get; set; }
        public bool IsRightMouseHeld { get; set; }
        
        public bool MiddleMousePressed { get; set; }
        public bool MiddleMouseReleased { get; set; }
        public bool IsMiddleMouseHeld { get; set; }
        
        // Scroll Inputs
        public Vector2 ScrollDelta { get; set; }
        
        // Utility
        public Ray ScreenPointRay { get; set; }
        
        public void Reset()
        {
            // Reset Frame States
            LeftMousePressed = false;
            LeftMouseReleased = false;
            
            RightMousePressed = false;
            RightMouseReleased = false;
            
            MiddleMousePressed = false;
            MiddleMouseReleased = false;
            
            ScrollDelta = Vector2.zero;
            PointerDelta = Vector2.zero;
        }

        public override string ToString()
        {
            return $"Pointer Position: {PointerPosition}, " +
                   $"Pointer Delta: {PointerDelta}, " +
                   $"Pointer World Position: {PointerWorldPosition}, " +
                   $"Left Mouse Pressed: {LeftMousePressed}, " +
                   $"Left Mouse Released: {LeftMouseReleased}, " +
                   $"Is Left Mouse Held: {IsLeftMouseHeld}, " +
                   $"Right Mouse Pressed: {RightMousePressed}, " +
                   $"Right Mouse Released: {RightMouseReleased}, " +
                   $"Is Right Mouse Held: {IsRightMouseHeld}, " +
                   $"Middle Mouse Pressed: {MiddleMousePressed}, " +
                   $"Middle Mouse Released: {MiddleMouseReleased}, " +
                   $"Is Middle Mouse Held: {IsMiddleMouseHeld}, " +
                   $"Scroll Delta: {ScrollDelta}";
        }
    }
}
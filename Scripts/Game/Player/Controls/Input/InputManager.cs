using System;
using Game.Player.Controls.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player.Controls.Input
{
    [DefaultExecutionOrder(-2)]
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        // References
        private InputSystem_Actions _inputActions;
        
        // State
        public InputContext CurrentInputContext { get; private set; } = new InputContext();

        // Singleton
        private static InputManager _instance;
        public static InputManager Instance
        {
            get => _instance ??= FindAnyObjectByType<InputManager>();
        }
        
        #region Unity Callbacks

        private void Awake()
        {
            // Initialize
            _mainCamera ??= Camera.main;
            _inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            SubscribeToInputEvents();
        }
        
        private void OnDisable()
        {
            _inputActions.Disable();
            UnsubscribeFromInputEvents();
        }

        private void OnDestroy()
        {
            // Clean Up
            if (_instance == this)
                _instance = null;
            
            _inputActions.Dispose();
        }

        private void LateUpdate()
        {
            CurrentInputContext.Reset();
        }

        #endregion
        
        #region Initialization
        
        private void SubscribeToInputEvents()
        {
            // Mouse Position
            _inputActions.Player.Point.performed += OnPointerMove;
            
            // Left Click
            _inputActions.Player.Click.started += OnLeftMouseDown;
            _inputActions.Player.Click.canceled += OnLeftMouseUp;
            
            // Right Click
            _inputActions.Player.RightClick.started += OnRightMouseDown;
            _inputActions.Player.RightClick.canceled += OnRightMouseUp;
            
            // Middle Click
            _inputActions.Player.MiddleClick.started += OnMiddleMouseDown;
            _inputActions.Player.MiddleClick.canceled += OnMiddleMouseUp;
            
            // Scroll Wheel
            _inputActions.Player.ScrollWheel.performed += OnScrollWheel;
        }
        
        private void UnsubscribeFromInputEvents()
        {
            // Mouse Position
            _inputActions.Player.Point.performed -= OnPointerMove;
            
            // Left Click
            _inputActions.Player.Click.started -= OnLeftMouseDown;
            _inputActions.Player.Click.canceled -= OnLeftMouseUp;
            
            // Right Click
            _inputActions.Player.RightClick.started -= OnRightMouseDown;
            _inputActions.Player.RightClick.canceled -= OnRightMouseUp;
            
            // Middle Click
            _inputActions.Player.MiddleClick.started -= OnMiddleMouseDown;
            _inputActions.Player.MiddleClick.canceled -= OnMiddleMouseUp;
            
            // Scroll Wheel
            _inputActions.Player.ScrollWheel.performed -= OnScrollWheel;
        }
        
        #endregion

        #region Input Event Handlers

        private void OnPointerMove(InputAction.CallbackContext context)
        {
            Vector2 position = context.ReadValue<Vector2>();
            Vector2 previousPosition = CurrentInputContext.PointerPosition;
            
            CurrentInputContext.PointerPosition = position;
            CurrentInputContext.PointerDelta = position - previousPosition;
            
            CurrentInputContext.PointerWorldPosition = _mainCamera.ScreenToWorldPoint(position);
            CurrentInputContext.ScreenPointRay = _mainCamera.ScreenPointToRay(position);
        }
        
        private void OnLeftMouseDown(InputAction.CallbackContext context)
        {
            CurrentInputContext.IsLeftMouseHeld = true;
            CurrentInputContext.LeftMousePressed = true;
        }
        
        private void OnLeftMouseUp(InputAction.CallbackContext context)
        {
            CurrentInputContext.IsLeftMouseHeld = false;
            CurrentInputContext.LeftMouseReleased = true;
        }
        
        private void OnRightMouseDown(InputAction.CallbackContext context)
        {
            CurrentInputContext.IsRightMouseHeld = true;
            CurrentInputContext.RightMousePressed = true;
        }
        
        private void OnRightMouseUp(InputAction.CallbackContext context)
        {
            CurrentInputContext.IsRightMouseHeld = false;
            CurrentInputContext.RightMouseReleased = true;
        }
        
        private void OnMiddleMouseDown(InputAction.CallbackContext context)
        {
            CurrentInputContext.IsMiddleMouseHeld = true;
            CurrentInputContext.MiddleMousePressed = true;
        }
        
        private void OnMiddleMouseUp(InputAction.CallbackContext context)
        {
            CurrentInputContext.IsMiddleMouseHeld = false;
            CurrentInputContext.MiddleMouseReleased = true;
        }
        
        private void OnScrollWheel(InputAction.CallbackContext context)
        {
            CurrentInputContext.ScrollDelta = context.ReadValue<Vector2>();
        }

        #endregion
    }
}
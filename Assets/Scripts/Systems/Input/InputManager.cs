using UnityEngine;
using UnityEngine.InputSystem;

namespace AgeOfKing.Systems.Input
{
    public class InputManager : MonoBehaviour
    {
        private IA_Gameplay _gameplayInput;

        //Entry point => mouse location changes
        public delegate void PointerLocationChanged(Vector2 location);
        public static PointerLocationChanged OnMousePositionChanged;
        public delegate void PointerLeftClick(Vector2 location);
        public static PointerLeftClick OnMouseLeftClicked;
        public delegate void PointerRightClick();
        public static PointerRightClick OnMouseRightClicked;

        private void OnEnable()
        {
            //Create
            _gameplayInput = new IA_Gameplay();
            _gameplayInput.Enable();

            //Subscribe
            _gameplayInput.Pointer.Position.performed += MousePosition_performed;
            _gameplayInput.Pointer.LeftClick.performed += LeftClick_performed;
            _gameplayInput.Pointer.RightClick.performed += RightClick_performed;
        }

        private void RightClick_performed(InputAction.CallbackContext obj)
        {
            OnMouseRightClicked?.Invoke();
        }

        private void LeftClick_performed(InputAction.CallbackContext obj)
        {
            Vector2 mouseLocation = _gameplayInput.Pointer.Position.ReadValue<Vector2>();
            OnMouseLeftClicked?.Invoke(mouseLocation);
        }

        private void MousePosition_performed(InputAction.CallbackContext obj)
        {
            Vector2 mouseLocation = obj.ReadValue<Vector2>();
            OnMousePositionChanged?.Invoke(mouseLocation);
        }

        private void OnDisable()
        {
            //Unsubscribe
            _gameplayInput.Pointer.Position.performed -= MousePosition_performed;
            _gameplayInput.Pointer.LeftClick.performed -= LeftClick_performed;
            _gameplayInput.Pointer.RightClick.performed -= RightClick_performed;
        }

    }

}

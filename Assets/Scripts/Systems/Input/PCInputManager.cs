using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AgeOfKing.Systems.Input
{
    public interface IInputManager 
    {
        public event Action<Vector2> OnPointerLocationChanged;
        public event Action<Vector2> OnPointerSelectionClicked;
        public event Action<Vector2> OnPointerCommandClicked;
        public void SwapInputState(bool isOn);

    }



    public class PCInputManager: IInputManager
    {
        private IA_Gameplay _gameplayInput;


        public event Action<Vector2> OnPointerLocationChanged;
        public event Action<Vector2> OnPointerSelectionClicked;
        public event Action<Vector2> OnPointerCommandClicked;

        public PCInputManager()
        {
            //Create
            _gameplayInput = new IA_Gameplay();
            _gameplayInput.Enable();
        }

        public void SwapInputState(bool isOn)
        {
            if (isOn == false)
            {
                DisableInputs();
                return;
            }

            _gameplayInput.Pointer.Position.performed += MousePosition_performed;
            _gameplayInput.Pointer.LeftClick.performed += LeftClick_performed;
            _gameplayInput.Pointer.RightClick.performed += RightClick_performed;
        }

        private void RightClick_performed(InputAction.CallbackContext obj)
        {
            Vector2 mouseLocation = _gameplayInput.Pointer.Position.ReadValue<Vector2>();
            OnPointerCommandClicked?.Invoke(mouseLocation);
        }

        private void LeftClick_performed(InputAction.CallbackContext obj)
        {
            Vector2 mouseLocation = _gameplayInput.Pointer.Position.ReadValue<Vector2>();
            OnPointerSelectionClicked?.Invoke(mouseLocation);
        }

        private void MousePosition_performed(InputAction.CallbackContext obj)
        {
            Vector2 mouseLocation = obj.ReadValue<Vector2>();
            OnPointerLocationChanged?.Invoke(mouseLocation);
        }

        void DisableInputs()
        {
            //Unsubscribe
            _gameplayInput.Pointer.Position.performed -= MousePosition_performed;
            _gameplayInput.Pointer.LeftClick.performed -= LeftClick_performed;
            _gameplayInput.Pointer.RightClick.performed -= RightClick_performed;
        }

    }

}

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public UnityEvent<Vector2Int> onMove;
    private InputActionPlayer _inputActionPlayer;

    private void Awake() => _inputActionPlayer = new InputActionPlayer();
    
    private void OnEnable()
    {
        _inputActionPlayer.Enable();
        _inputActionPlayer.Controller.Move.performed += OnMovePerformed;
    }

    private void OnDisable()
    {
        _inputActionPlayer.Controller.Move.performed -= OnMovePerformed;
        _inputActionPlayer.Disable();
    }


    private void OnMovePerformed(InputAction.CallbackContext callbackContext)
    {
        onMove.Invoke(callbackContext.ReadValue<Vector2Int>());
    }
    
}
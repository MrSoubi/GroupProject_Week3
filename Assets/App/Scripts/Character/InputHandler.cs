using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public UnityEvent<Vector2> onMove;

    private InputActionPlayer _inputActionPlayer;
    private bool _canSwipe = true;
    private float _magnitudeThreshold = 5f;

    private void Awake()
    {
        _inputActionPlayer = new InputActionPlayer();
    }

    private void OnEnable()
    {
        _inputActionPlayer.Enable();
        _inputActionPlayer.Controller.Move.performed += OnMovePerformed;
        _inputActionPlayer.Controller.Touch.performed += OnTouchPerformed;
        _inputActionPlayer.Controller.Touch.canceled += OnTouchCancelled;
    }

    private void OnDisable()
    {
        _inputActionPlayer.Controller.Move.performed -= OnMovePerformed;
        _inputActionPlayer.Controller.Touch.performed -= OnTouchPerformed;
        _inputActionPlayer.Controller.Touch.canceled -= OnTouchCancelled;
        _inputActionPlayer.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext callbackContext)
    {
        onMove.Invoke(callbackContext.ReadValue<Vector2>());
    }

    private void OnTouchPerformed(InputAction.CallbackContext callbackContext)
    {
        _inputActionPlayer.Controller.Swipe.performed += OnSwipePerformed;
    }

    private void OnTouchCancelled(InputAction.CallbackContext callbackContext)
    {
        _inputActionPlayer.Controller.Swipe.performed -= OnSwipePerformed;
        _canSwipe = true;
    }

    private void OnSwipePerformed(InputAction.CallbackContext callbackContext)
    {
        if (!_canSwipe)
        {
            return;
        }

        Vector2 swipe = callbackContext.ReadValue<Vector2>();

        if (swipe.magnitude < _magnitudeThreshold)
        {
            return;
        }

        Vector2 moveDirection = Vector2.zero;
        if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            moveDirection = new Vector2(swipe.x / Mathf.Abs(swipe.x), 0);
        }
        else
        {
            moveDirection = new Vector2(0, swipe.y / Mathf.Abs(swipe.y));
        }

        onMove.Invoke(moveDirection);

        _canSwipe = false;
    }
}

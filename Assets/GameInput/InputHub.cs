using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputHub : MonoBehaviour
{
    private GameInput _gameInput;
    
    private bool _isStickPressed = false;
    private Coroutine _readInputCoroutine;
    
    public static event EventHandler <OnStickHoldEventArgs>OnStickHold;
    public static event EventHandler OnStickHoldReleased;

    public static event EventHandler < OnScreenContactEventArgs> OnScreenContact;
    public class  OnScreenContactEventArgs : EventArgs
    {
        public Vector3 TouchWorldPosition;
    }
    
    public class  OnStickHoldEventArgs:EventArgs
    {
        public Vector2 LeftStickVector;
    }
    
    private void OnEnable()
    {
        _gameInput= new GameInput();
        _gameInput.Enable();

        _gameInput.Player.JoyStickTap.started += OnStickPressed;

        _gameInput.Player.JoyStickTap.canceled += OnStickReleased;
        _gameInput.Player.ScreenTouch.started += OnScreenTouch;
    }

    private void OnScreenTouch(InputAction.CallbackContext obj)
    {
        
       // if (EventSystem.current.IsPointerOverGameObject())
       //     return;

        var screenPosition = _gameInput.Player.ScreenTouchPoint.ReadValue<Vector2>();

      
        if (IsTouchOverUI(screenPosition))
            return;

        var worldPosition = ScreenToWorldPosition(screenPosition);

        OnScreenContact?.Invoke(this, new OnScreenContactEventArgs { TouchWorldPosition = worldPosition });
    }

    private bool IsTouchOverUI(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    private Vector3 ScreenToWorldPosition(Vector2 screenPosition)
    {
        Vector3 worldPosition = Vector3.zero; 
        
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            worldPosition = hit.point; 
        }

        return worldPosition;
    }

    private void OnStickReleased(InputAction.CallbackContext obj)
    {
       _isStickPressed = false;
       _readInputCoroutine= null;
       OnStickHoldReleased?.Invoke(this, EventArgs.Empty);
    }

    private void OnStickPressed(InputAction.CallbackContext obj)
    {
        _isStickPressed = true;

       _readInputCoroutine= StartCoroutine(ReadStickInput());
    }

    private IEnumerator ReadStickInput()
    {
        while (_isStickPressed)
        { 
            var stickVector = _gameInput.Player.Movement.ReadValue<Vector2>();
            OnStickHold?.Invoke(this, new OnStickHoldEventArgs { LeftStickVector =stickVector  });
            yield return null;
        }
    }

    private void OnDisable()
    {
        _gameInput.Player.JoyStickTap.started -= OnStickPressed;
        _gameInput.Player.JoyStickTap.canceled -= OnStickReleased;
        _gameInput.Player.ScreenTouch.started -= OnScreenTouch;
    }
}

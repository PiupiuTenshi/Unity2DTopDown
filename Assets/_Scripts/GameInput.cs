using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    public event EventHandler OnTabAction;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Menu.performed += Menu_performed;
    }

    private void Menu_performed(InputAction.CallbackContext context)
    {
        OnTabAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementNormalize()
    {
        Vector2 inputAction = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputAction = inputAction.normalized;
        return inputAction;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Menu.performed -= Menu_performed;

        playerInputActions.Dispose();
    }
}

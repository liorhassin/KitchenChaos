using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;
    public void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        
        playerInputActions.Player.Interact.performed += Interact_performed; //Adding listener to Interact action.
    }

    private void Interact_performed(InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}

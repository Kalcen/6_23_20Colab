using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SplineMesh;

public class PlayerController : MonoBehaviour
{
    #region properties & variables

    Controls controls;
    [SerializeField, Range(0,10)]
    float horizontalSpeed;
    Vector2 movementInput;
    Vector3 desiredVelocity;
    Vector3 velocity;

    #endregion

    //--UNITY EVENT FUNCTIONS

    private void Awake() => controls = new Controls();

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Movement.performed += ctx => OnMovement(ctx);
        controls.Player.Movement.canceled += ctx => OnMovement(ctx);
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Movement.performed -= ctx => OnMovement(ctx);
        controls.Player.Movement.canceled -= ctx => OnMovement(ctx);
    }

    private void Update()
    {
        desiredVelocity.x = movementInput.x * horizontalSpeed;

        transform.Translate(desiredVelocity * Time.deltaTime);
    }

    //--METHODS

    void OnMovement(InputAction.CallbackContext ctx) 
    {
        switch (ctx.phase) 
        {
            case InputActionPhase.Performed:
                movementInput.x = ctx.ReadValue<Vector2>().x;
                break;
            case InputActionPhase.Canceled:
                movementInput.x = 0;
                break;
        }
    }
}

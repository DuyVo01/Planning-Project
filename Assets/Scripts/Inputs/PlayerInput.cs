using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 movementInput;
    public Vector3 MovementVector { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.Player.Move.performed += Move;
        playerControls.Player.Move.canceled += Move;
    }

    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.Player.Move.performed -= Move;
        playerControls.Player.Move.canceled -= Move;
    }

    private void Update()
    {
        CalculateMovementDirectionToCamera();
    }

    private void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
        MovementVector = new Vector3(movementInput.x, 0, movementInput.y);
    }

    private void CalculateMovementDirectionToCamera()
    {
        // Calculate the camera-relative direction
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement vector using the input data
        Vector3 movementDirection = (cameraForward * movementInput.y) + (cameraRight * movementInput.x);
        movementDirection.y = 0f; // Ensure the movement stays in the horizontal plane
        MovementVector = movementDirection.normalized;
    }
}

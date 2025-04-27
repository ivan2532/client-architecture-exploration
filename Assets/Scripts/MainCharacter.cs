using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float speed = 5f;

    private GameInputActions _inputActions;
    private Vector2 _movementInput;

    private void Awake()
    {
        _inputActions = new GameInputActions();
    }

    private void OnEnable()
    {
        _inputActions.MainCharacter.Move.performed += OnMovePerformed;
        _inputActions.MainCharacter.Move.canceled += OnMoveCanceled;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.MainCharacter.Move.performed -= OnMovePerformed;
        _inputActions.MainCharacter.Move.canceled -= OnMoveCanceled;
        _inputActions.Disable();
    }
    
    private void FixedUpdate()
    {
        var movement = new Vector3(_movementInput.x, 0f, _movementInput.y).normalized * speed;
        playerRigidbody.linearVelocity = new Vector3(movement.x, playerRigidbody.linearVelocity.y, movement.z);
    }
    
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        if (gameState.Paused) return;
        _movementInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (gameState.Paused) return;
        _movementInput = Vector2.zero;
    }
}
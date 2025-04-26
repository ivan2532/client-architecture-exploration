using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float speed = 5f;

    private InputActions _inputActions;
    private Vector2 _movementInput;

    private void Awake()
    {
        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _movementInput = Vector2.zero;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
    
    private void FixedUpdate()
    {
        var movement = new Vector3(_movementInput.x, 0f, _movementInput.y).normalized * speed;
        playerRigidbody.linearVelocity = new Vector3(movement.x, playerRigidbody.linearVelocity.y, movement.z);
    }
}
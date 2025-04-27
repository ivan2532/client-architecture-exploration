using UnityEngine;
using UnityEngine.InputSystem;

public class Drone : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Camera droneCamera;
    [SerializeField] private Collider dummyTarget;
    [SerializeField] private Hud hud;
    
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Vector2 pitchRange;
    [SerializeField] private Vector2 yawRange;
    
    [SerializeField] private Transform mainCharacter;
    [SerializeField] private float smoothTime;
    
    private GameInputActions _inputActions;
    private float _pitch;
    private float _yaw;
    
    private Vector3 _offsetFromTarget;
    private Vector3 _velocity;

    private void Awake()
    {
        _inputActions = new GameInputActions();
    }
    
    private void OnEnable()
    {
        _inputActions.Drone.Look.performed += OnLookPerformed;
        _inputActions.Drone.Shoot.performed += OnShootPerformed;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
    
    private void Start()
    {
        InitializeOrientation();
        CursorUtility.HideCursor();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void InitializeOrientation()
    {
        _pitch = transform.rotation.eulerAngles.x;
        _yaw = transform.rotation.eulerAngles.y;
        _offsetFromTarget = transform.position - mainCharacter.position;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        if (gameState.Paused) return;
        
        var orientationDelta = context.ReadValue<Vector2>();
        _pitch = Mathf.Clamp(_pitch - orientationDelta.y * lookSensitivity, pitchRange.x, pitchRange.y);
        _yaw = Mathf.Clamp(_yaw + orientationDelta.x * lookSensitivity, yawRange.x, yawRange.y);
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (gameState.Paused) return;
        
        var raycastHitNotEmpty = Physics.Raycast(
            droneCamera.transform.position, 
            droneCamera.transform.forward, 
            out var hit);
        
        if (raycastHitNotEmpty && hit.collider == dummyTarget)
        {
            hud.IncrementScore();
            Debug.Log("Drone has shot dummy target!");
        }
    }

    private void UpdateRotation()
    {
        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private void FollowTarget()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            mainCharacter.position + _offsetFromTarget, 
            ref _velocity, 
            smoothTime);
    }
}

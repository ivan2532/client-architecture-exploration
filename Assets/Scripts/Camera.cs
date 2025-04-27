using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Vector2 pitchRange;
    [SerializeField] private Vector2 yawRange;
    
    [SerializeField] private Transform followTarget;
    [SerializeField] private float smoothTime;
    
    private InputActions _inputActions;
    private float _pitch;
    private float _yaw;

    private Vector3 _offsetFromTarget;
    private Vector3 _velocity;

    private void Awake()
    {
        _inputActions = new InputActions();
    }
    
    private void OnEnable()
    {
        _inputActions.Player.Look.performed += OnLookPerformed;
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
    
    private void Start()
    {
        InitializeOrientation();
        HideCursor();
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
        _offsetFromTarget = transform.position - followTarget.position;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        var orientationDelta = context.ReadValue<Vector2>();
        _pitch = Mathf.Clamp(_pitch - orientationDelta.y * lookSensitivity, pitchRange.x, pitchRange.y);
        _yaw = Mathf.Clamp(_yaw + orientationDelta.x * lookSensitivity, yawRange.x, yawRange.y);
    }

    private void UpdateRotation()
    {
        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private void FollowTarget()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            followTarget.position + _offsetFromTarget, 
            ref _velocity, 
            smoothTime);
    }
}

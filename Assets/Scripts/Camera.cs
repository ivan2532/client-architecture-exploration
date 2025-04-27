using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float smoothTime;

    private Vector3 _offsetFromTarget;
    private Vector3 _velocity;
    
    private void Start()
    {
        _offsetFromTarget = transform.position - followTarget.position;
    }

    private void LateUpdate()
    {
        FollowTarget();
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

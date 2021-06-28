using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    
    private Camera _mainCamera;
    private Rigidbody _rb;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Touchscreen.current.primaryTouch.press.IsPressed())
        {
            var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            var worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

            _movementDirection = transform.position - worldPosition;
            _movementDirection.z = 0;
            _movementDirection.Normalize();
        }
        else
        {
            _movementDirection = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (_movementDirection == Vector3.zero) return;
        
        _rb.AddForce(_movementDirection * (forceMagnitude * Time.deltaTime), ForceMode.Force);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxVelocity);
    }
}

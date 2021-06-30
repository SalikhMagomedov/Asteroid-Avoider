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
        ProcessInput();
        KeepPlayerOnScreen();
    }

    private void FixedUpdate()
    {
        if (_movementDirection == Vector3.zero) return;

        _rb.AddForce(_movementDirection * (forceMagnitude * Time.deltaTime), ForceMode.Force);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxVelocity);
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            var worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

            _movementDirection = transform.position - worldPosition;
            _movementDirection.z = 0f;
            _movementDirection.Normalize();
        }
        else
        {
            _movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen()
    {
        var position = transform.position;
        var viewportPosition = _mainCamera.WorldToViewportPoint(position);

        if (viewportPosition.x > 1) position.x = -position.x + .1f;
        else if (viewportPosition.x < 0) position.x = -position.x - .1f;
        else if (viewportPosition.y > 1) position.y = -position.y + .1f;
        else if (viewportPosition.y < 0) position.y = -position.y - .1f;

        transform.position = position;
    }
}
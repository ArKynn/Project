using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    private PlayerInteraction _playerInteraction;
    private PlayerBodyGrab _playerBodyGrab;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] [Range(0f, 1f)] private float breakPercentage;
    [SerializeField] private float rotationSpeed;
    private Vector2 _movementVector;

    public Vector3 MovementVector
    {
        get => new (_movementVector.x, 0, _movementVector.y); 
        private set => _movementVector = new Vector2(value.x, value.z);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerBodyGrab = GetComponent<PlayerBodyGrab>();
        _playerInteraction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        _movementVector = _input.actions["Move"].ReadValue<Vector2>();
        if(_input.actions["Action"].WasPressedThisFrame()) HandleContextAction();
    }

    private void HandleContextAction()
    {
        if (_playerBodyGrab.CarryingBody) _playerBodyGrab.ThrowBody();
        else _playerInteraction.Interact();
    }

    private void FixedUpdate()
    {
        RotateBody();
        Move();
    }

    private void RotateBody()
    {
        if(MovementVector.magnitude == 0) return;
        var targetRotation = Quaternion.LookRotation(MovementVector, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (MovementVector.magnitude != 0)
        {
            if (_rigidbody.linearVelocity.magnitude < maxSpeed)
            {
                MovementVector *= movementSpeed;
                var movementForce = MovementVector;
                _rigidbody.AddForce(movementForce, ForceMode.Force);
            }
        }
        else
        {
            var breakForce = breakPercentage * _rigidbody.linearVelocity.magnitude * -_rigidbody.linearVelocity;
            _rigidbody.AddForce(breakForce, ForceMode.Force);
            if(Mathf.Approximately(_rigidbody.linearVelocity.magnitude, 0)) _rigidbody.linearVelocity = Vector3.zero;
        }
        _rigidbody.angularVelocity = Vector3.zero;
    }
}

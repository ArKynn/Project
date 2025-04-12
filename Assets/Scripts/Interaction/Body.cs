using UnityEngine;

public class Body : InteractiveObject
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override bool Interact()
    {
        ToggleRigidBody();
        return true;
    }

    private void ToggleRigidBody()
    {
        _rb.isKinematic = !_rb.isKinematic;
        _rb.detectCollisions = !_rb.detectCollisions;
        
    }
}
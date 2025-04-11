using System;
using UnityEngine;

public class PlayerBodyCarry : MonoBehaviour
{
    [Header("Throw Settings")]
    [SerializeField] private float throwForce;
    [SerializeField] [Range(0f, 90f)] private float yThrowAngle;
    
    private Body _carryBody;
    public bool CarryingBody => _carryBody != null;

    public void ThrowBody()
    {
        var bodyRb = _carryBody.GetComponent<Rigidbody>();
        if(bodyRb != null) bodyRb.AddForce(GetThrowVector() * throwForce, ForceMode.Impulse);

        _carryBody = null;
        OnBodyThrow?.Invoke(this, EventArgs.Empty);
    }

    private Vector3 GetThrowVector()
    {
        return Quaternion.AngleAxis(yThrowAngle, Vector3.right) * transform.forward;
    }

    public void CarryBody(Body body)
    {
        _carryBody = body;
        OnBodyCarry?.Invoke(this, EventArgs.Empty);
    }
    
    public event EventHandler OnBodyCarry;
    public event EventHandler OnBodyThrow;
}

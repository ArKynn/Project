using System;
using UnityEngine;

public class PlayerBodyGrab : MonoBehaviour
{
    [Header("Body Grab Settings")]
    [SerializeField] private GameObject grabParent;
    [SerializeField] private Vector3 grabOffset;
    
    [Header("Throw Settings")]
    [SerializeField] private float throwForce;
    [SerializeField] [Range(0f, 90f)] private float yThrowAngle;
    
    private Body _grabBody;
    public bool CarryingBody => _grabBody != null;

    public void ThrowBody()
    {
        if(_grabBody == null) return;
        
        var bodyRb = _grabBody.GetComponent<Rigidbody>();
        if (bodyRb != null)
        {
            _grabBody.transform.parent = null;
            bodyRb.AddForce(GetThrowVector() * throwForce, ForceMode.Impulse);
        }

        _grabBody = null;
        OnBodyThrow?.Invoke(this, EventArgs.Empty);
    }

    private Vector3 GetThrowVector()
    {
        return Quaternion.AngleAxis(yThrowAngle, Vector3.right) * transform.forward;
    }

    public void GrabBody(Body body)
    {
        if(body == null) return;
        
        _grabBody = body;
        _grabBody.transform.SetParent(grabParent.transform);
        _grabBody.transform.localPosition = grabOffset;
        OnBodyGrab?.Invoke(this, EventArgs.Empty);
    }
    
    public event EventHandler OnBodyGrab;
    public event EventHandler OnBodyThrow;
}

using System;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private InputSystem_Actions _input;
    private Rigidbody _robotRb;

    public float movementSpeed;
    public float rotationSpeed;

    private void Start()
    {
        // _input = new InputSystem_Actions();
        
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            _robotRb = rb;
        }
    }

    private void Update()
    {
        var sideForce = Input.GetAxis("Horizontal") * movementSpeed;
        if (sideForce != 0)
        {
            _robotRb.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }
        
        var forwardForce = Input.GetAxis("Vertical") * movementSpeed;
        if (forwardForce != 0)
        {
            _robotRb.linearVelocity = _robotRb.transform.forward * forwardForce;
        }
    }

    // private void OnEnable()
    // {
    //     _input.Player.Enable();
    // }
    //
    // private void OnDisable()
    // {
    //     _input.Player.Disable();
    // }
}
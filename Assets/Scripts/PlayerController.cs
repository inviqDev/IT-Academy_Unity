using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform crosshairPosition;

    private ProjectilesSpawner _projectilesSpawner;
    private CharacterController _controller;
    private InputSystem_Actions _actions;
    private Action<CallbackContext> _attackCallBack;

    private readonly Vector3 _gravity = new(0.0f, -9.81f, 0.0f);
    public CharacterController Controller => _controller ??= GetComponent<CharacterController>();

    public float moveSpeed;
    public float sensitivity;

    private float _crosshairRotationY;

    private void Update()
    {
        if (_actions == null) return;

        var moveDelta = _actions.Player.Move.ReadValue<Vector2>();
        var lookDelta = _actions.Player.Look.ReadValue<Vector2>();
        
        MovePlayer(moveDelta);
        RotatePlayerX(lookDelta);
    }

    private void MovePlayer(Vector2 moveDelta)
    {
        var sideDirection = Vector3.zero;
        if (moveDelta != Vector2.zero)
        {
            var forwardDirection = playerBody.forward * moveDelta.y;
            var rightDirection = playerBody.right * moveDelta.x;
            sideDirection = (forwardDirection + rightDirection) * moveSpeed;
        }

        var moveDirection = (_gravity + sideDirection) * Time.deltaTime;
        Controller.Move(transform.TransformDirection(moveDirection));
    }

    private void RotatePlayerX(Vector2 lookDelta)
    {
        if (lookDelta == Vector2.zero) return;

        var lookRotationX = lookDelta.x * sensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up, lookRotationX);

        var lookRotationY = -lookDelta.y * sensitivity * Time.deltaTime;
        RotateCrosshairY(lookRotationY);
    }

    private void RotateCrosshairY(float rotationY)
    {
        _crosshairRotationY += rotationY;

        _crosshairRotationY = Mathf.Clamp(_crosshairRotationY, -20f, 20f);
        crosshairPosition.localEulerAngles = new Vector3(_crosshairRotationY, 0f, 0f);
    }

    private void LaunchProjectile(CallbackContext context)
    {
        _projectilesSpawner ??= GetComponent<ProjectilesSpawner>();
        
        if (!_projectilesSpawner)
        {
            Debug.Log("Projectiles spawner component wasn't found on the Player");
            return;
        }
        
        _projectilesSpawner.LaunchNextProjectile();
    }

    private void OnEnable()
    {
        _actions ??= new InputSystem_Actions();
        _actions.Enable();

        _attackCallBack = LaunchProjectile;
        _actions.Player.Attack.started += _attackCallBack;
    }

    private void OnDisable()
    {
        if (_actions == null || _attackCallBack == null) return;
        _actions.Player.Attack.canceled -= LaunchProjectile;

        _actions?.Disable();
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _controller;

        private Input_Actions _inputActions;
        private Vector2 _moveInput;

        [Header("Movement")] 
        [SerializeField] private float moveSpeed = 10f;

        [Header("Gravity")] public float gravity = -14f;
        private float _verticalVelocity;

        [Header("Jump")] 
        [SerializeField] private float jumpForce = 6.5f;
        [SerializeField] private float upMultiplier = 1.4f;
        [SerializeField] private float downMultiplier = 1.8f;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            SignEvents();
        }

        private void OnMoveOnPerformed(InputAction.CallbackContext ctx)
        {
            _moveInput = ctx.ReadValue<Vector2>();  
        }

        private void OnMoveOnCanceled(InputAction.CallbackContext ctx)
        {
            _moveInput = Vector2.zero;
        }

        private void OnJumpOnPerformed(InputAction.CallbackContext ctx)
        {
            if (_controller.isGrounded)
            {
                _verticalVelocity = jumpForce;
            }   
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            if (_controller.isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = -2f;
            }
            else
            {
                if (_verticalVelocity > 0)
                {
                    _verticalVelocity += gravity * (upMultiplier - 1.0f) * Time.deltaTime;
                }
                else if (_verticalVelocity < 0)
                {
                    _verticalVelocity += gravity * (downMultiplier - 1.0f) * Time.deltaTime;
                }

                _verticalVelocity += gravity * Time.deltaTime;
            }

            var moveDirection = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
            moveDirection = transform.TransformDirection(moveDirection * moveSpeed);
            moveDirection.y = _verticalVelocity;

            _controller.Move(moveDirection * Time.deltaTime);
        }

        private void OnDisable()
        {
            UnsignEvents();
        }

        private void OnDestroy()
        {
            UnsignEvents();
        }

        private void SignEvents()
        {
            _inputActions ??= new Input_Actions();

            _inputActions.Gameplay.Move.performed += OnMoveOnPerformed;
            _inputActions.Gameplay.Move.canceled += OnMoveOnCanceled;

            _inputActions.Gameplay.Jump.performed += OnJumpOnPerformed;

            _inputActions.Enable();
        }

        private void UnsignEvents()
        {
            if (_inputActions == null) return;

            _inputActions.Gameplay.Move.performed -= OnMoveOnPerformed;
            _inputActions.Gameplay.Move.canceled -= OnMoveOnCanceled;

            _inputActions.Gameplay.Jump.performed -= OnJumpOnPerformed;

            _inputActions?.Disable();
        }
    }
}
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform cameraRoot;

        [Header("Settings")] 
        [SerializeField] private float mouseSensitivity = 5.0f;
        [SerializeField] private float verticalClamp = 80f;

        private Input_Actions _inputActions;
        private Vector2 _lookDelta;
        private float _xRotation;
        
        private bool _isFiring;
        private Coroutine _fireRoutine;

        private void OnEnable()
        {
            _inputActions ??= new Input_Actions();

            _inputActions.Gameplay.Look.performed += OnLookOnPerformed;
            _inputActions.Gameplay.Look.canceled += LookOnCanceled;
            
            _inputActions.Gameplay.Attack.performed += AttackOnPerformed;
            _inputActions.Gameplay.Attack.canceled += AttackOnCanceled;
            
            _inputActions.Enable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, _lookDelta.x * mouseSensitivity, Space.Self);

            _xRotation -= _lookDelta.y * mouseSensitivity;
            _xRotation = Mathf.Clamp(_xRotation, -verticalClamp, verticalClamp);
            cameraRoot.localEulerAngles = new Vector3(_xRotation, 0f, 0f);
        }
        
        private void AttackOnCanceled(InputAction.CallbackContext obj)
        {
            _isFiring = false;
            
            if (_fireRoutine == null) return;
            StopCoroutine(_fireRoutine);    
            _fireRoutine = null;
        }

        private void AttackOnPerformed(InputAction.CallbackContext obj)
        {
            var currentStandWeapon = PlayerManager.Instance.CurrentWeapon;
        
            if (!currentStandWeapon) return;
            if (_isFiring) return;

            _isFiring = true;
            _fireRoutine = StartCoroutine(FireRoutine());
        }

        private void OnLookOnPerformed(InputAction.CallbackContext ctx)
        {
            _lookDelta = ctx.ReadValue<Vector2>();
        }
        
        private void LookOnCanceled(InputAction.CallbackContext obj)
        {
            _lookDelta = Vector2.zero;
        }
        
        private IEnumerator FireRoutine()
        {
            var currentWeapon = PlayerManager.Instance.CurrentWeapon;
            if (!currentWeapon) yield break;
            
            while (_isFiring)
            {
                var attackDirection = PlayerManager.Instance.CrosshairRoot.transform.forward;
                PlayerManager.Instance.CurrentWeapon.UseWeapon(attackDirection);
                
                yield return new WaitForSeconds(currentWeapon.FireRate);
            }
        }

        private void OnDisable()
        {
            if (_inputActions == null) return;

            _inputActions.Gameplay.Look.performed -= OnLookOnPerformed;
            _inputActions.Gameplay.Look.canceled -= LookOnCanceled;
            
            _inputActions.Gameplay.Attack.performed += AttackOnPerformed;
            _inputActions.Gameplay.Attack.canceled += AttackOnCanceled;

            _inputActions?.Disable();
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDestroy()
        {
            if (_inputActions == null) return;

            _inputActions.Gameplay.Look.performed -= OnLookOnPerformed;
            _inputActions.Gameplay.Look.canceled -= LookOnCanceled;
            
            _inputActions.Gameplay.Attack.performed += AttackOnPerformed;
            _inputActions.Gameplay.Attack.canceled += AttackOnCanceled;

            _inputActions?.Disable();
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
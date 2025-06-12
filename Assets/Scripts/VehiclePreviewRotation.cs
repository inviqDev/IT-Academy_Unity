using UnityEngine;
using UnityEngine.EventSystems;

public class VehiclePreviewRotation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private VehiclesContainer vehiclesContainer;
    private Transform _currentVehicle;

    private InputSystem_Actions _actions;

    private bool _isPointerDown;
    public float rotationSpeed = 0.2f;

    private void Update()
    {
        if (!_isPointerDown) return;
        
        var deltaX = _actions.Player.RotateY.ReadValue<Vector2>().x;
        _currentVehicle.Rotate(Vector3.up, -deltaX * rotationSpeed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _currentVehicle = vehiclesContainer.ActiveVehicle.transform;
        _isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }

    private void OnEnable()
    {
        _actions ??= new InputSystem_Actions();
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions?.Disable();
    }
}
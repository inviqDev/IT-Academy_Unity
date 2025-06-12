using UnityEngine;
using UnityEngine.UI;

public class VehicleSwitcher : MonoBehaviour
{
    [SerializeField] private VehiclesContainer vehiclesContainer;
    [SerializeField] private SideViewCamera sideCamera;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private void OnLeftButtonClick()
    {
        vehiclesContainer.SwitchVehicle(-1);
        sideCamera.SetDefaultVehiclePositionToRenderTexture();
    }
    
    private void OnRightButtonClick()
    {
        vehiclesContainer.SwitchVehicle(1);
        sideCamera.SetDefaultVehiclePositionToRenderTexture();
    }

    private void OnEnable()
    {
        leftButton.onClick.AddListener(OnLeftButtonClick);
        rightButton.onClick.AddListener(OnRightButtonClick);
    }

    private void OnDisable()
    {
        leftButton.onClick.RemoveListener(OnLeftButtonClick);
        rightButton.onClick.RemoveListener(OnRightButtonClick);
    }
}
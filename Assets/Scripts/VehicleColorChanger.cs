using UnityEngine;
using UnityEngine.UI;

public class VehicleColorChanger : MonoBehaviour
{
    [SerializeField] private VehiclesContainer vehiclesContainer;

    [SerializeField] private Button redButton;
    [SerializeField] private Button blueButton;
    [SerializeField] private Button yellowButton;
    [SerializeField] private Button greenButton;

    private void ChangeColor(Color color)
    {
        if (!vehiclesContainer && vehiclesContainer.VehiclesList.Count < 1)
        {
            Debug.Log("There are no vehicles");
            return;
        }

        for (var i = 0; i < vehiclesContainer.VehiclesList.Count; i++)
        {
            if (!vehiclesContainer.VehiclesList[i].activeInHierarchy) continue;
            if (!vehiclesContainer.VehiclesList[i].TryGetComponent<MeshRenderer>(out var meshRenderer))
            {
                Debug.Log($"The active vehicle {vehiclesContainer.VehiclesList[i]} has no mesh renderer component");
            }

            meshRenderer.material.color = color;
            break;
        }
    }

    private void OnRedButtonClicked() => ChangeColor(Color.red);
    private void OnBlueButtonClicked() => ChangeColor(Color.blue);
    private void OnYellowButtonClicked() => ChangeColor(Color.yellow);
    private void OnGreenButtonClicked() => ChangeColor(Color.green);

    private void OnEnable()
    {
        redButton.onClick.AddListener(OnRedButtonClicked);
        blueButton.onClick.AddListener(OnBlueButtonClicked);
        yellowButton.onClick.AddListener(OnYellowButtonClicked);
        greenButton.onClick.AddListener(OnGreenButtonClicked);
    }

    private void OnDisable()
    {
        redButton.onClick.RemoveListener(OnRedButtonClicked);
        blueButton.onClick.RemoveListener(OnBlueButtonClicked);
        yellowButton.onClick.RemoveListener(OnYellowButtonClicked);
        greenButton.onClick.RemoveListener(OnGreenButtonClicked);
    }
}
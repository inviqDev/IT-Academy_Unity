using System.Collections.Generic;
using UnityEngine;

public class VehiclesContainer : MonoBehaviour
{
    public List<GameObject> VehiclesList { get; private set; }
    public GameObject ActiveVehicle { get; private set; }
    public Vector3 DefaultPosition { get; private set; }
    public Quaternion DefaultRotation { get; private set; }
    public int ActiveVehicleIndex { get; private set; }

    private void Start()
    {
        if (transform.childCount <= 0) return;

        VehiclesList = new List<GameObject>();
        for (var i = 0; i < transform.childCount; i++)
        {
            VehiclesList.Add(transform.GetChild(i).gameObject);

            if (i == 0)
            {
                VehiclesList[i].SetActive(true);

                ActiveVehicleIndex = i;
                ActiveVehicle = VehiclesList[ActiveVehicleIndex];
                
                DefaultPosition = ActiveVehicle.transform.position;
                DefaultRotation = ActiveVehicle.transform.rotation;
            }
            else
                VehiclesList[i].SetActive(false);
        }
    }

    public void SwitchVehicle(int indexChanger)
    {
        if (VehiclesList == null || VehiclesList.Count == 0) return;

        SetVehicleDefaultPosition();
        VehiclesList[ActiveVehicleIndex].SetActive(false);

        var nextIndex = indexChanger switch
        {
            -1 => ActiveVehicleIndex == 0
                ? VehiclesList.Count - 1
                : ActiveVehicleIndex + indexChanger,

            1 => ActiveVehicleIndex == VehiclesList.Count - 1
                ? 0
                : ActiveVehicleIndex + indexChanger,

            _ => 0
        };

        SetVehicleDefaultPosition();
        VehiclesList[nextIndex].SetActive(true);

        ActiveVehicleIndex = nextIndex;
        ActiveVehicle = VehiclesList[ActiveVehicleIndex];
    }

    private void SetVehicleDefaultPosition()
    {
        ActiveVehicle.transform.position = DefaultPosition;
        ActiveVehicle.transform.rotation = DefaultRotation;
    }
}
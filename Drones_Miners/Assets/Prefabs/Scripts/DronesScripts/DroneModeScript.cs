using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneModeScript : MonoBehaviour
{
    [SerializeField] private GameObject workImage;
    [SerializeField] private GameObject searchImage;
    [SerializeField] private GameObject deliveryImage;

    public void SetMode(DroneMode droneMode)
    {
        workImage.SetActive(false); searchImage.SetActive(false); deliveryImage.SetActive(false);

        switch(droneMode)
        {
            case DroneMode.Work:
                workImage.SetActive(true);break;
            case DroneMode.Search:
                searchImage.SetActive(true);break;
            case DroneMode.Delivery:
                deliveryImage.SetActive(true);break;
        }
    }
}
public enum DroneMode
{
    None,
    Work,
    Search,
    Delivery
}

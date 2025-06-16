using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeScript : MonoBehaviour
{
    protected static GameObject focuseDrone;

    public static void ChangeCameraState(GameObject drone)
    {
        if(drone == focuseDrone) { focuseDrone = null; return; }
        focuseDrone = drone;
    }

    private void Update()
    {
        if (focuseDrone != null)
        {
            transform.position = new Vector3(focuseDrone.transform.position.x, 10f, focuseDrone.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(0,25f,0);
        }
    }
}
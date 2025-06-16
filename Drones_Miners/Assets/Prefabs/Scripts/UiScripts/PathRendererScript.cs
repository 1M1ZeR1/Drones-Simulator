using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRendererScript : MonoBehaviour
{
    private Dictionary<GameObject, LineRenderer> droneAndTherePath = new Dictionary<GameObject, LineRenderer>();
    private Dictionary<GameObject, NavMeshAgent> droneToAgent = new Dictionary<GameObject, NavMeshAgent>();

    protected bool _inPathRenderMode = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (_inPathRenderMode && droneAndTherePath.Count!=0)
        {
            foreach(var drone in droneAndTherePath.Keys)
            {
                if (droneToAgent[drone].hasPath)
                {
                    droneAndTherePath[drone].positionCount = droneToAgent[drone].path.corners.Length;
                    droneAndTherePath[drone].SetPositions(droneToAgent[drone].path.corners);
                }
                else { droneAndTherePath[drone].positionCount = 0; }
            }
        }
    }

    public void AddDrones(List<GameObject> allDrones)
    {
        foreach(var drone in allDrones)
        {
            droneAndTherePath.Add(drone, drone.GetComponent<LineRenderer>());
            droneToAgent.Add(drone,drone.GetComponent<NavMeshAgent>());
        }
    }
    public void ClearAllDictionary()
    {
        droneAndTherePath?.Clear();
        droneToAgent?.Clear();
    }
    public void ChangeRenderMode(bool renderMode) 
    {
        if (!renderMode)
        {
            foreach(var lineRenderes in droneAndTherePath.Values) { lineRenderes.positionCount = 0; }
        }
        _inPathRenderMode = renderMode; 
    }
}

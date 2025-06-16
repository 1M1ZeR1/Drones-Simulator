using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DronesSpawnerScript : MonoBehaviour
{
    [Header("Объект дрона")]
    [SerializeField] private GameObject droneObject;

    [SerializeField] private GameObject pathRendererObject;
    private PathRendererScript pathRendererScript;

    [SerializeField] private GameObject[] spawnPlaces;

    [SerializeField] private Material sideMaterial;

    protected List<GameObject> _allDrones = new List<GameObject>();

    private int countOfDrons = 3;

    private int speedPreset = 3;

    [SerializeField] private Side side;

    private void Start()
    {
        pathRendererObject.TryGetComponent(out pathRendererScript);

        StartCoroutine(SpawnDrones(countOfDrons));
    }

    public IEnumerator SpawnDrones(int needSpawn)
    {
        WaitForSeconds spawnInterval = new WaitForSeconds(0.2f);

        for(int i = 0; i < needSpawn; i++)
        {
            GameObject newDrone = Instantiate(droneObject, spawnPlaces[i].transform.position,Quaternion.identity);

            newDrone.GetComponent<DroneControllerScript>().SetBase(spawnPlaces[i]);
            newDrone.GetComponent<DroneControllerScript>().SetSide(side);
            newDrone.GetComponent<MeshRenderer>().material = sideMaterial;

            newDrone.GetComponent<NavMeshAgent>().speed = speedPreset;

            _allDrones.Add(newDrone);
            countOfDrons++;

            newDrone.SetActive(true);

            yield return spawnInterval;
        }

        pathRendererScript.AddDrones(_allDrones);
    }

    public void ChangeCountOfDrones(float value)
    {
        StopAllCoroutines();

        pathRendererScript.ClearAllDictionary();

        foreach(var drone in _allDrones)
        {
            drone.GetComponent<DroneControllerScript>().ClearAllEvents();
            Destroy(drone);
        }

        _allDrones.Clear();
        countOfDrons = 0;

        StartCoroutine(SpawnDrones((int)value));
    }

    public void ChangeSpeed(float speed)
    {
        speedPreset = (int)speed;

        foreach (var drone in _allDrones)
        {
            drone.GetComponent<NavMeshAgent>().speed = speedPreset;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawnerScript : MonoBehaviour
{
    [Header("Объект ресурса")]
    [SerializeField] private GameObject resourceObject;

    [Header("Интервал спавна ресурса")]
    [SerializeField] private int intervalToSpawn;

    [Header("Максимальное кол-во ресурсов на карте")]
    [SerializeField] private int maxCountOfResources;

    public delegate void ResourceHasSpawned(GameObject resource);
    public event ResourceHasSpawned OnSpawned;

    public delegate void ResourceCollected(GameObject drone);
    public event ResourceCollected OnCollected;

    public delegate void ClearTargetResource(GameObject resource);
    public event ClearTargetResource OnClearTargetResource;

    protected int _countOfResources = 0;

    protected int _radius = 10;

    protected float _timer = 0;

    protected List<GameObject> _resourcesOnField = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= intervalToSpawn)
        {
            _timer = 0;

            if(_countOfResources == maxCountOfResources ) { return; }
            SpawnResource();
        }
    }

    private void SpawnResource()
    {
        Vector3 randomSpawnPlace = Random.insideUnitSphere * _radius;

        randomSpawnPlace = new Vector3(randomSpawnPlace.x, transform.position.y, randomSpawnPlace.z);

        GameObject newResource = Instantiate(resourceObject, randomSpawnPlace, Quaternion.identity, transform);

        newResource.SetActive(true);

        _countOfResources++;

        _resourcesOnField.Add(newResource);

        OnSpawned?.Invoke(newResource);
    }

    public void SetIntervalToSpawn(string intervalToSpawn)
    {
        if (int.TryParse(intervalToSpawn, out int newInterval))
        {
            this.intervalToSpawn = newInterval;
        }
    }

    public void CollectResource(GameObject collectedResource, GameObject drone)
    {

        Destroy(collectedResource.GetComponent<SphereCollider>());

        _countOfResources--;

        _resourcesOnField.Remove(collectedResource);

        OnClearTargetResource?.Invoke(collectedResource);

        StartCoroutine(CollectResourceWithTime(collectedResource, drone));
    }
    private IEnumerator CollectResourceWithTime(GameObject resource, GameObject drone)
    {
        yield return new WaitForSeconds(2f);

        Destroy(resource);

        OnCollected?.Invoke(drone);
    }

    public List<GameObject> GetAllResources()
    {
        return _resourcesOnField;
    }
}

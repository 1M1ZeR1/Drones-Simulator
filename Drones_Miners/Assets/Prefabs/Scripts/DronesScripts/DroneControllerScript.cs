using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneControllerScript : MonoBehaviour
{
    private Side side = Side.None;

    [SerializeField] private GameObject resourcesSpawnerObject;
    private ResourcesSpawnerScript resourcesSpawnerScript;

    [SerializeField] private GameObject scoreControllerObject;
    private ScoreController scoreControllerScript;

    private DroneModeScript droneModeScript;

    private NavMeshAgent agent;

    protected GameObject _targetResource;
    protected GameObject _base;

    protected bool _withResource = false;
    protected bool _inCollected = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        droneModeScript = GetComponent<DroneModeScript>();

        resourcesSpawnerObject.TryGetComponent(out resourcesSpawnerScript);
        scoreControllerObject.TryGetComponent(out scoreControllerScript);

        resourcesSpawnerScript.OnSpawned += SetTargetResource;
        resourcesSpawnerScript.OnClearTargetResource += ClearTargetResource;
    }

    private void Update()
    {
        if(_targetResource == null && !_inCollected)
        {
            droneModeScript.SetMode(DroneMode.Search);

            float distance = float.MaxValue;

            GameObject chosenObject = null;

            foreach(var resource in resourcesSpawnerScript.GetAllResources())
            {
                if(Vector3.Distance(transform.position,resource.transform.position) < distance)
                {
                    chosenObject = resource;
                    distance = Vector3.Distance(transform.position, resource.transform.position);
                }
            }

            if(chosenObject != null) { SetTargetResource(chosenObject); }
        }
        if (_withResource && !agent.hasPath) { agent.SetDestination(_base.transform.position); }
    }

    private void SetTargetResource(GameObject resource)
    {
        if (!_withResource && !_inCollected)
        {
            if (_targetResource == null) { _targetResource = resource; agent.SetDestination(resource.transform.position); }
            else
            {
                if (Vector3.Distance(transform.position, _targetResource.transform.position) > Vector3.Distance(transform.position, resource.transform.position))
                {
                    _targetResource = resource;
                    agent.SetDestination(resource.transform.position);
                }
            }
        }
    }
    public void SetBase(GameObject droneBase) { _base = droneBase; }
    public void SetSide(Side side) { this.side = side; }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Resource") && !_inCollected)
        {
            resourcesSpawnerScript.CollectResource(collision.gameObject, gameObject);

            droneModeScript.SetMode(DroneMode.Work);

            _inCollected = true;

            resourcesSpawnerScript.OnCollected += HandleResourceCollected;
        }
        if (collision.gameObject.CompareTag("Base") && _withResource)
        {
            StartCoroutine(ResourceDischarge());
        }
    }
    private void ClearTargetResource(GameObject resource) { if(_targetResource == resource && !_inCollected)_targetResource = null; }
    private void HandleResourceCollected(GameObject drone)
    {
        if (drone == gameObject) 
        { 
            agent.SetDestination(_base.transform.position); 
            _withResource = true;

            scoreControllerScript.AddScore(side);

            droneModeScript.SetMode(DroneMode.Delivery);

            resourcesSpawnerScript.OnCollected -= HandleResourceCollected; }
    }

    private IEnumerator ResourceDischarge()
    {
        yield return new WaitForSeconds(2);

        _withResource = false;
        _inCollected = false;

        droneModeScript.SetMode(DroneMode.Search);
    }

    public void ClearAllEvents()
    {
        resourcesSpawnerScript.OnSpawned -= SetTargetResource;
        resourcesSpawnerScript.OnClearTargetResource -= ClearTargetResource;
        resourcesSpawnerScript.OnCollected -= HandleResourceCollected;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}

public enum Side
{
    None,
    Blue,
    Red
}

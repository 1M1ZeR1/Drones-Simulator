using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorsSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject meteor;

    [SerializeField] private int spawnInterval;

    protected float _timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > spawnInterval)
        {
            _timer = 0;

            SpawnMeteor();
        }
    }

    private void SpawnMeteor()
    {
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, Random.Range(-15f, 15f));

            GameObject newMeteor = Instantiate(meteor, spawnPosition, Quaternion.identity, transform);

            newMeteor.SetActive(true);
        }
    }
}

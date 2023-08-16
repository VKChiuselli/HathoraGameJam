using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeaponPowerUpManager : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> listOfObjectToSpawn;
    void Start()
    {
        spawnPoints = new List<Transform>();

        foreach (Transform spawnPoint in this.transform)
        {
            spawnPoints.Add(spawnPoint);
        }
        lastSpawnTime = Time.time;

        SpawnObject();
        SpawnObject();
        SpawnObject();
        SpawnObject();
        SpawnObject();
        SpawnObject();
    }


    public float spawnDelay = 15f;

    private float lastSpawnTime;




    private void Update()
    {
        if (Time.time - lastSpawnTime >= spawnDelay)
        {
            lastSpawnTime = Time.time;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        if (spawnPoints.Count == 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, spawnPoints.Count);
        int randomItemIndex = Random.Range(0, listOfObjectToSpawn.Count);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(listOfObjectToSpawn[randomItemIndex], spawnPoint.position, spawnPoint.rotation);
        spawnPoints.Remove(spawnPoint);

    }

}

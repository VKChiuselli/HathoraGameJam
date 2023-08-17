using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnWeaponPowerUpManager : NetworkBehaviour
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

    }


    public float spawnDelay = 2f;

    private float lastSpawnTime;




    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        if (Time.time - lastSpawnTime >= spawnDelay)
        {
            lastSpawnTime = Time.time;
            SpawnObject();
        }
    }

    GameObject itemToSpawn;

     
    private void SpawnObject ()
    {
        if (spawnPoints.Count == 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, spawnPoints.Count);
        int randomItemIndex = Random.Range(0, listOfObjectToSpawn.Count);
        Transform spawnPoint = spawnPoints[randomIndex];

        itemToSpawn = Instantiate(listOfObjectToSpawn[randomItemIndex], spawnPoint.position, spawnPoint.rotation);

        NetworkObject itemToSpawnNetworkObject = itemToSpawn.GetComponent<NetworkObject>();
        itemToSpawnNetworkObject.Spawn();

        spawnPoints.Remove(spawnPoint);

    }

}

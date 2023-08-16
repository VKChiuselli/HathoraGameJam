using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : NetworkBehaviour
{
    private int currentWaypoint = 0;
    [SerializeField] private Transform[] waypoints;
    public static BossAI Instance { get; private set; }
    private GameObject[] players = { };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
    //    GetComponent<NavMeshAgent>().SetDestination(waypoints[currentWaypoint].position);
    }


    public void OnPlayerJoined()
    {
        Debug.Log("player joined" + IsServer);
        if (!IsServer) return;
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    public void OnPlayerLeft()
    {
        Debug.Log("player left" + IsServer);
        if (!IsServer) return;
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsServer) return;
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.1)
        {
            if (++currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            GetComponent<NavMeshAgent>().SetDestination(waypoints[currentWaypoint].position);
        }

        foreach (GameObject player in players)
        {
            if (player == null) continue;
            Debug.Log("distance" + Vector3.Distance(player.transform.position, transform.position));
            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    private int currentWaypoint = 0;
    [SerializeField] private Transform[] waypoints;


    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(waypoints[currentWaypoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.1)
        {
            if (++currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            GetComponent<NavMeshAgent>().SetDestination(waypoints[currentWaypoint].position);
        }
    }
}

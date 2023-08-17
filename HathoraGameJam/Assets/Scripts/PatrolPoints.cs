using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField]    private int currentPoint = -1;
    [SerializeField] List<Transform> pointsWhereBossGo; //make this enumerator
    [SerializeField] public Transform deskPoint; //make this enumerator
    [SerializeField] Transform bossWaypointsParent;


    private void Awake()
    {
        if (pointsWhereBossGo.Count < 1)
        {
            GetBossWaypoints();
        }
    }

    private void GetBossWaypoints()
    {
        pointsWhereBossGo.Clear(); // Clear the list before populating

        // Loop through all children of bossWaypointsParent and add them to pointsWhereBossGo list
        for (int i = 0; i < bossWaypointsParent.childCount; i++)
        {
            Transform child = bossWaypointsParent.GetChild(i);
            pointsWhereBossGo.Add(child);
        }
    }

    public Transform GetNext()
    {
        if (++currentPoint >= pointsWhereBossGo.Count)
        {
            currentPoint = 0;
        }
        return pointsWhereBossGo[currentPoint];
    }

    public bool HasReached(NavMeshAgent navMeshAgent)
    {
        return currentPoint == -1 || navMeshAgent.remainingDistance == 0;
    }

    public bool HeadingToDesk(NavMeshAgent navMeshAgent)
    {
        return navMeshAgent.destination == deskPoint.position;
    }

}

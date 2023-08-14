using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField]
    private int currentPoint = -1;
    [SerializeField] private List<Transform> points; //make this enumerator
    [SerializeField] public Transform deskPoint; //make this enumerator


    public Transform GetNext()
    {
        if (++currentPoint >= points.Count)
        {
            currentPoint = 0;
        }
        return points[currentPoint];
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

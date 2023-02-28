using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    [Header("Nav Mesh")]
    [SerializeField]
    NavMeshAgent navMeshAgent;
    [SerializeField]
    Transform[] waypoints;
    [SerializeField]
    int currentWaypoint;

    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }

    }
}

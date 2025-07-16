using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] private Transform[] waypoints;
    private int waypointIndex;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Start()
    {
        waypoints = FindFirstObjectByType<WaypointManager>().GetWaypoints();
    }

    private void Update()
    {

        FaceTarget(agent.steeringTarget);

        //check if agent is close to current target point
        if (agent.remainingDistance < .5f)
        {
            // Set destination to next waypoint
            agent.SetDestination(GetNextWaypoint());
        }
    }

    private void FaceTarget(Vector3 newTarget)
    {
        // Calculate the direction from current position to the new target
        Vector3 directionToTarget = newTarget - transform.position;
        directionToTarget.y = 0; // Ignore vertical

        Quaternion newRotation = Quaternion.LookRotation(directionToTarget);

        // Smoothly rotate from current rotation to target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }

    private Vector3 GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.Length)
        {
            //waypointIndex = 0;
            return transform.position;
        }
        Vector3 targetPoint = waypoints[waypointIndex].position;
        waypointIndex++;

        return targetPoint;
    }
}

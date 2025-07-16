using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType {Basic, Fast, None}

public class Enemy : MonoBehaviour, IDamagable
{
    private NavMeshAgent agent;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform centerPoint;
    public int healthPoints = 4;

    [Header("Movement")]
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] private Transform[] waypoints;
    private int waypointIndex;

    private float totalDistance;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Start()
    {
        waypoints = FindFirstObjectByType<WaypointManager>().GetWaypoints();
        CollectTotalDistance();
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

    public float DistanceToFinishLine() => totalDistance + agent.remainingDistance;

    private void CollectTotalDistance()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            float distance = Vector3.Distance(waypoints[i].position, waypoints[i + 1].position);
            totalDistance = totalDistance + distance;
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

        //Get target point from waypoint array 
        Vector3 targetPoint = waypoints[waypointIndex].position;

        if (waypointIndex > 0)
        {
            float distance = Vector3.Distance(waypoints[waypointIndex].position, waypoints[waypointIndex - 1].position);
            totalDistance -= distance;
        }

        waypointIndex++;

        return targetPoint;
    }

    public Vector3 CenterPoint() => centerPoint.position;

    public EnemyType GetEnemyType() => enemyType;

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
            Destroy(gameObject);
    }
}

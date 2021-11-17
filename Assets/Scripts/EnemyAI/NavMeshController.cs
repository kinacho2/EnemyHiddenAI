using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshController : MonoBehaviour
{
    public bool IsMoving => !agent.isStopped && agent.remainingDistance > 0.5f;

    private NavMeshAgent agent;
    private NavMeshPath _tempPath;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StartMovement(Vector3 position)
    {
        agent.SetDestination(position);
        agent.isStopped = false;
    }

    public void StopMovement()
    {        
        agent.isStopped = true;
    }

    public void ResumeMovement()
    {
        agent.isStopped = false;
    }

    public float GetTravelDistanceToTarget(Vector3 position, out bool isPositionValid)
    {
        _tempPath ??= new NavMeshPath();

        isPositionValid = agent.CalculatePath(position, _tempPath);
        
        float distance = 0;
        
        // If we have at least one corner
        if (isPositionValid && _tempPath.corners.Length > 0)
        {
            // Add the distance between waypoints
            for (int i = 1; i < _tempPath.corners.Length; i++)
            {
                distance += Vector3.Distance(_tempPath.corners[i - 1], _tempPath.corners[i]);
            }
        }

        return distance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolPoints;
    private int currentPatrolPoint;

    private NavMeshAgent agent;

    [SerializeField] private Transform target;
    private Vector3 defaultTargetPosition;

    private EnemySight sight;

    [SerializeField] UnityEvent onPlayerSighted;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        currentPatrolPoint = 0;
        GoToNextPoint();

        defaultTargetPosition = target.localPosition;
        sight = GetComponent<EnemySight>();
    }

    private void Update()
    {
        if (ReachedDestinationOrGaveUp())
        {
            // GoToNextPoint();
        }

        if (sight.IsObjectInSight())
        {
            target.position = sight.GetEnemy().position;
            onPlayerSighted?.Invoke();
        }
        else
        {
            target.localPosition = defaultTargetPosition;
        }
    }

    private void GoToNextPoint()
    {
        if (patrolPoints.Count == 0)
        {
            return;
        }

        agent.destination = patrolPoints[currentPatrolPoint].position;

        currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Count;
    }

    private bool ReachedDestinationOrGaveUp()
    {
        //if (agent != null && !agent.pathPending)
        //{
        //    if (agent.remainingDistance <= agent.stoppingDistance)
        //    {
        //        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
        //        {
        //            return true;
        //        }
        //    }
        //}

        return false;
    }
}

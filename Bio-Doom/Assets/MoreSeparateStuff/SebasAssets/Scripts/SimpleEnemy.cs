using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
    }

    private State state;

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 targetMovePosition;

    float targetRange = 50f;

    private Vector3 startingPoint;

    EnemyNavMeshHandler enemyPathFinding;

    private void Awake()
    {
        state = State.Roaming;
    }

    private void Start()
    {
        enemyPathFinding = GetComponent<EnemyNavMeshHandler>();
        startingPoint = transform.position;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Debug.Log("State Roaming");
                enemyPathFinding.RoamingMovement();
                FindTarget();
                break;
            case State.ChaseTarget:
                Debug.Log("State Chasing");
                enemyPathFinding.ChasingMethod(player.transform);
                OutOfRange();
                break;
        }
    }

    private void FindTarget()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {
            state = State.ChaseTarget;
        }
    }

    private void OutOfRange()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > targetRange)
        {
            state = State.Roaming;
        }
    }
}

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
        Attack,
    }

    private State state;

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 targetMovePosition;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] float targetRange, targetInCloseRange;

    private Vector3 startingPoint;

    EnemyNavMeshHandler enemyPathFinding;

    private Animator enemyAnimator;

    private bool playerInRange = false;

    private void Awake()
    {
        state = State.Roaming;
    }

    private void Start()
    {
        enemyPathFinding = GetComponent<EnemyNavMeshHandler>();
        startingPoint = transform.position;
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        

        switch (state)
        {
            default:
            case State.Roaming:
                Debug.Log("State Roaming");

                enemyPathFinding.RoamingMovement();
                enemyAnimator.SetTrigger("Walking");

                FindTarget();

                break;

            case State.ChaseTarget:
                Debug.Log("State Chasing");

                enemyAnimator.SetTrigger("Running");

                enemyPathFinding.ChasingMethod(player.transform);
                playerInRange = Physics.CheckSphere(transform.position, targetInCloseRange, playerLayer);

                TargetCloseRange();
                OutOfRange();

                //enemyAnimator.SetTrigger("RunningExit");

                break;

            case State.Attack:
                Debug.Log("State Attack");

                AttackPlayer();
                playerInRange = Physics.CheckSphere(transform.position, targetInCloseRange, playerLayer);
                OutOfRange();

                break;
        }
    }

    private void AttackPlayer()
    {
        enemyAnimator.SetTrigger("Attack");
        enemyPathFinding.navMeshAgent.SetDestination(transform.position);
    }

    private void FindTarget()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {
            state = State.ChaseTarget;
        }
    }

    private void TargetCloseRange()
    {

        if(playerInRange == true)
        {
            state = State.Attack;
        }
    }

    private void OutOfRange()
    {
        if (state == State.ChaseTarget)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > targetRange)
            {
                state = State.Roaming;
            }
        }
        else if(state == State.Attack)
        {
            if (!playerInRange)
            {
                state = State.ChaseTarget;
            }
        }
    }
}

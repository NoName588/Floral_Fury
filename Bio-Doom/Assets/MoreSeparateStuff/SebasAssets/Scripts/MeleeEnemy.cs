using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    private enum State
    {
        Idle,
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
        state = State.Idle;
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
            case State.Idle:
                Debug.Log("State Idling");

                //enemyPathFinding.RoamingMovement();
                //enemyAnimator.SetTrigger("Idle");

                FindTarget();

                break;

            case State.ChaseTarget:
                Debug.Log("State Chasing");

                //enemyAnimator.SetTrigger("Running");
                enemyPathFinding.navMeshAgent.isStopped = false;
                enemyAnimator.SetTrigger("Running");
                enemyPathFinding.ChasingMethod(player.transform.position);
                playerInRange = Physics.CheckSphere(transform.position, targetInCloseRange, playerLayer);
                //enemyAnimator.SetTrigger("Running");
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
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        enemyAnimator.SetTrigger("Attack");
        //enemyPathFinding.navMeshAgent.SetDestination(transform.position);
        enemyPathFinding.navMeshAgent.isStopped = true; //If it is "true", then the navmesh Agent will not move and reset the destination
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < targetRange)
        {
            state = State.ChaseTarget;
        }
    }

    private void TargetCloseRange()
    {

        if (playerInRange == true)
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
                enemyAnimator.ResetTrigger("Running");
                enemyAnimator.SetTrigger("RunningExit");
                enemyPathFinding.navMeshAgent.isStopped = true;
                state = State.Idle;
            }
        }
        else if (state == State.Attack)
        {
            if (!playerInRange)
            {
                enemyAnimator.ResetTrigger("Attack");
                enemyAnimator.SetTrigger("AttackExit");
                enemyPathFinding.navMeshAgent.isStopped = false; //If it is false, then it can move freely
                state = State.ChaseTarget;
            }
        }
    }
}

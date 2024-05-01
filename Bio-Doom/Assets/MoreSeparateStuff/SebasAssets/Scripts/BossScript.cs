using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        enemyPathFinding = GetComponent<EnemyNavMeshHandler>();
        startingPoint = transform.position;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                Debug.Log("State Idling");

                //enemyPathFinding.RoamingMovement();
                enemyAnimator.SetTrigger("Running");

                FindTarget();

                break;

            case State.ChaseTarget:
                Debug.Log("State Chasing");

                //enemyAnimator.SetTrigger("Running");

                enemyPathFinding.ChasingMethod(player.transform);
                playerInRange = Physics.CheckSphere(transform.position, targetInCloseRange, playerLayer);
                enemyAnimator.SetTrigger("Running");
                TargetCloseRange();
                OutOfRange();

                //enemyAnimator.SetTrigger("RunningExit");

                break;

            case State.Attack:
                Debug.Log("SAttack");

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
                enemyAnimator.SetTrigger("Running");
                state = State.Idle;
            }
        }
        else if (state == State.Attack)
        {
            if (!playerInRange)
            {
                enemyAnimator.SetTrigger("Attack");
                state = State.ChaseTarget;
            }
        }
    }
}

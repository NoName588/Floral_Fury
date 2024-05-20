using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
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
    public float timeReleseAttack = 5f;
    public Vector2 randomRange, waitRandomRange;
    private bool playerInRange = false, death;
    public Collider[] collArms;
    private float currentRandomAttack, timetoStop;
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
                if (death) return;
                enemyPathFinding.RoamingMovement();
                enemyAnimator.SetTrigger("Walking");
                ColliderManager(false);
                FindTarget();

                break;

            case State.ChaseTarget:
                Debug.Log("State Chasing");
                if (death) return;
                enemyAnimator.SetTrigger("Running");

                enemyPathFinding.ChasingMethod(player.transform.position);
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

    private void ColliderManager(bool enabled)
    {
        foreach (Collider collider in collArms)
            collider.isTrigger = enabled;
    }

    private void AttackPlayer()
    {
        

        if (timetoStop >= timeReleseAttack)
            StartCoroutine(WaitAttack());
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < targetInCloseRange)
            {
                ColliderManager(true);
                currentRandomAttack = Random.Range(randomRange.x, randomRange.y);
                enemyAnimator.SetFloat("RandomAttack", currentRandomAttack);

                if (currentRandomAttack < 0.5f)
                    enemyAnimator.SetTrigger("Attack");
                else
                    enemyAnimator.SetTrigger("Attack2");

                enemyPathFinding.navMeshAgent.SetDestination(player.transform.position + new Vector3(0, 0, 2f));

                timetoStop += Time.deltaTime;
            }
            else
                ColliderManager(false);
        }
    }

    private IEnumerator WaitAttack()
    {
        death = true;
        enemyAnimator.SetBool("TimeToStop", death);
        yield return new WaitForSeconds(Random.Range(waitRandomRange.x, waitRandomRange.y));
        death = false;
        enemyAnimator.SetBool("TimeToStop", death);
        timetoStop = 0f;
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < targetRange)
            state = State.ChaseTarget;
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
                state = State.Roaming;
            }
        }
        else if (state == State.Attack)
        {
            if (!playerInRange)
            {
                state = State.ChaseTarget;
            }
        }
    }
}

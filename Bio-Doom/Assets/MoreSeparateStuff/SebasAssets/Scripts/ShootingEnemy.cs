using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    private enum State
    {
        Idle,
        ChaseTarget,
        Shoot,
    }

    private State state;

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 targetMovePosition;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] float targetRange, targetInCloseRange;

    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float timer = 5f;
    private float bulletTime;

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

                FindTarget();

                break;

            case State.ChaseTarget:
                Debug.Log("State Chasing");

                enemyPathFinding.navMeshAgent.isStopped = false;
                enemyAnimator.SetTrigger("Running");
                enemyPathFinding.ChasingMethod(player.transform.position);
                playerInRange = Physics.CheckSphere(transform.position, targetInCloseRange, playerLayer);
                //enemyAnimator.SetTrigger("Running");
                TargetCloseRange();
                OutOfRange();

                break;

            case State.Shoot:
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
        enemyAnimator.SetTrigger("Shooting");
        ShootPlayer();
        enemyPathFinding.navMeshAgent.isStopped = true; //If it is "true", then the navmesh Agent will not move and reset the destination
    }

    private void ShootPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObject = Instantiate(enemyBullet, spawnPosition.transform.position, spawnPosition.transform.rotation);
        Rigidbody bulletRB = bulletObject.GetComponent<Rigidbody>();

        //bulletRB.AddForce(new Vector3(0f, 0f, 1f) * 1000);
        bulletRB.AddForce(bulletRB.transform.forward * 1000);
        Destroy(bulletObject, 5f);

        /*RaycastHit hit;

        if (Physics.Raycast(spawnPosition.position, Vector3.forward, out hit, targetInCloseRange))
        {
            Debug.DrawRay(spawnPosition.position, Vector3.forward * hit.distance, Color.red);
        }*/
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
            state = State.Shoot;
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
        else if (state == State.Shoot)
        {
            if (!playerInRange)
            {
                enemyAnimator.ResetTrigger("Shooting");
                enemyAnimator.SetTrigger("ShootingExit");
                enemyPathFinding.navMeshAgent.isStopped = false; //If it is false, then it can move freely
                state = State.ChaseTarget;
            }
        }
    }
}

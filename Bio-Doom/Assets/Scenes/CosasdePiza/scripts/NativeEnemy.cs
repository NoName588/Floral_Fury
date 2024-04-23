using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NativeEnemy : MonoBehaviour
{
    public Transform centerAlphaRadius;
    public Vector2 patrolStandRangeTime = new Vector2(1.5f, 3f);
    public float patrolRadius = 5f;
    public float patrolSpeed = 6f;
    public float alphaPatrolRadius = 15f;
    public float attackRadius = 2f;
    public float detectionRadius = 10f;
    public float detectionAngle = 90f;



    public float chaseSpeed = 6f;
    public float timePatrol = 5f;
    private Transform player;
    private NavMeshAgent agent;
    private bool isChasing;
    private bool isAttanking;
    private float currentTimePatrol = 0;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!isChasing)
            {
                Vector3 randomPoint = Random.insideUnitSphere * alphaPatrolRadius;
                NavMeshHit hit;
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (Vector3.Distance(centerAlphaRadius.position, randomPoint) <= alphaPatrolRadius)
                    {
                        if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
                            if (Vector3.Distance(centerAlphaRadius.position, randomPoint) <= patrolRadius)
                                agent.SetDestination(hit.position);
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(patrolStandRangeTime.x, patrolStandRangeTime.y));
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
            Vector3 behindPlayer = player.position - player.forward * 2f; // Posición detrás del jugador
            agent.SetDestination(behindPlayer);
        }
        else
        {
            currentTimePatrol += Time.deltaTime; 
            isChasing = false;
            agent.speed = patrolSpeed;
            if (currentTimePatrol >= )
        }
    }

    private void OnDrawGizmosSelected()
    {
        DrawWireSphere(Color.blue, centerAlphaRadius.position, alphaPatrolRadius);
        DrawWireSphere(Color.yellow, transform.position, patrolRadius);
        DrawWireSphere(Color.red, transform.position, attackRadius);
        DrawWireSphere(Color.magenta, transform.position, detectionRadius);
    }

    private void DrawWireSphere(Color color, Vector3 center, float radius)
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(center, radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NativeEnemy : MonoBehaviour
{
    public enum NativeEnemyStates { Patroling, Chasing, Attacking };
    public NativeEnemyStates nativeState = NativeEnemyStates.Patroling;
    public Vector2 patrolStandRangeTime = new Vector2(1f, 2f);
    public float alphaPatrolRadius = 10f;
    public float patrolRadius = 5f;
    public float patrolSpeed = 6f;
    public float attackRadius = 2f;
    public float detectionRadius = 10f;
    public float detectionAngle = 90f;
    public float chaseSpeed = 6f;
    public float timePatrol = 5f;
    public float angularSpeed = 6f;
    public bool setCenter = true;

    private Transform player;
    private NavMeshAgent agent; 
    private Vector3 centerPoint, attackDirection;
    private float currentTimePatrol = 0;
    private bool isChasing;
    private bool isAttanking;
    private bool doOnceCoroutine = true;

    void Start()
    {
        centerPoint = transform.position;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        StateMachine();
        LookForPlayer();
    }

    private void StateMachine()
    {
        switch (nativeState)
        {
            case NativeEnemyStates.Patroling:
                Patroling();
            break;
            case NativeEnemyStates.Chasing:
                Chasing();
                break;
            case NativeEnemyStates.Attacking:
                Attacking();
            break;
            default:
            break;
        }
    }

    private void LookForPlayer()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = player.position - enemyPosition;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                nativeState = NativeEnemyStates.Chasing;
            else
            {
                doOnceCoroutine = true;
                setCenter = true;
                nativeState = NativeEnemyStates.Patroling;
            }
        }
        else
        {
            doOnceCoroutine = true;
            setCenter = true;
            nativeState = NativeEnemyStates.Patroling;
        }
    }

    private void Patroling()
    {
        agent.speed = patrolSpeed;
        if (doOnceCoroutine)
        {
            doOnceCoroutine = false;
            StartCoroutine(GoToRandomPoint());
        }
    }

    private IEnumerator GoToRandomPoint()
    {
        while (true)
        {
            if (nativeState == NativeEnemyStates.Patroling)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Vector3 point;
                    if (RandomPoint(centerPoint, patrolRadius, out point))
                    {
                        float distanceToPoint = Vector3.Distance(transform.position, point);
                        if (distanceToPoint <= patrolRadius)
                        {
                            Debug.DrawLine(point, Vector2.one, Color.blue, 1.0f);
                            agent.SetDestination(point);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(patrolStandRangeTime.x, patrolStandRangeTime.y));
        }
    }

    private void Chasing()
    {
        LookRotationTarget();
        isChasing = true;
        agent.speed = chaseSpeed;
        Vector3 behindPlayer = player.position - player.forward * attackRadius; // Posición detrás del jugador
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(behindPlayer);
            nativeState = NativeEnemyStates.Attacking;
        }
    }

    private void Attacking()
    {
        LookRotationTarget();
    }

    private void LookRotationTarget()
    {
        attackDirection = player.position - transform.position;
        attackDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(attackDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * angularSpeed);
    }


    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (setCenter)
        {
            setCenter = false;
            centerPoint = transform.position;
        }
        else
        {
            DrawWireDisc(Color.green, centerPoint, transform.up, alphaPatrolRadius);
            DrawWireDisc(Color.yellow, transform.position, transform.up, patrolRadius);
            DrawWireDisc(Color.red, transform.position, transform.up, attackRadius);
            DrawWireDisc(Color.magenta, transform.position, transform.up, detectionRadius);
            UnityEditor.Handles.color = new Color(0.8f, 0, 0.8f, 0.4f);
            Vector3 rotatedForward = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, detectionAngle, detectionRadius);
        }
    }
    
    private void DrawWireDisc(Color color, Vector3 center, Vector3 normal, float radius)
    {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawWireDisc(center, normal, radius);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum NativeEnemyStates { Patroling, Chasing, Attacking };
    public NativeEnemyStates nativeState = NativeEnemyStates.Patroling;
    public enum AttackTypeStates { Melee, Distance }
    public AttackTypeStates nativeAttackState = AttackTypeStates.Melee;
    public bool resetCenter = true;
    public List<Enemy> enemiesList = new List<Enemy>();
    public BaseEnemySettings settings;

    protected Transform player;
    protected NavMeshAgent agent;
    protected Vector3 centerPoint, attackDirection, playerChasePosition;
    protected float currentAttackRadius;
    protected bool doOnceCoroutine = true;
    protected Color[] colorsFov = new Color[2];
   
    void Start()
    {
        centerPoint = transform.position;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        nativeAttackState = enemiesList.Count == 0 ? Enemy.AttackTypeStates.Melee : Enemy.AttackTypeStates.Distance;
        if (resetCenter)
            StartCoroutine(ResetCenter());

        StateMachine();
        LookForPlayer();
    }

    protected IEnumerator ResetCenter()
    {
        resetCenter = false;
        centerPoint = transform.position;
        yield return null;
    }

    protected void StateMachine()
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
        }
    }

    protected void LookForPlayer()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = player.position - enemyPosition;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= settings.detectionRadius)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(settings.detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                resetCenter = true;
                colorsFov[0] = settings.colors[5];
                colorsFov[1] = settings.colors[5];
                nativeState = NativeEnemyStates.Chasing;

                if (toPlayer.magnitude <= currentAttackRadius + 0.1f)
                    nativeState = NativeEnemyStates.Attacking;
            }
            else
            {
                colorsFov[0] = settings.colors[6];
                colorsFov[1] = settings.colors[6];
                nativeState = NativeEnemyStates.Patroling;
            }
        }
        else
        {
            colorsFov[0] = settings.colors[6];
            colorsFov[1] = settings.colors[6];
            nativeState = NativeEnemyStates.Patroling;
        }
    }

    private void Patroling()
    {
        agent.speed = settings.patrolSpeed;
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
                    if (RandomPoint(centerPoint, settings.alphaPatrolRadius, out point))
                    {
                        float distanceToPoint = Vector3.Distance(transform.position, point);
                        if (distanceToPoint <= settings.patrolRadius)
                        {
                            Debug.DrawLine(point, Vector2.one, Color.blue, 1.0f);
                            agent.SetDestination(point);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(settings.patrolStandRangeTime.x, settings.patrolStandRangeTime.y));
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, settings.patrolRadius, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    private void Chasing()
    {
        LookRotationTarget();
        agent.speed = settings.chaseSpeed;
        switch (nativeAttackState)
        {
            case AttackTypeStates.Melee:
                playerChasePosition = player.position;
                agent.stoppingDistance = settings.meleeAttackRadius;
                currentAttackRadius = settings.meleeAttackRadius;
                break;
            case AttackTypeStates.Distance:
                playerChasePosition = playerChasePosition = player.position - player.forward * settings.distanceAttackRadius; // Posición detrás del jugador
                agent.stoppingDistance = 0f;
                currentAttackRadius = settings.distanceAttackRadius;
                break;
        }
        agent.SetDestination(playerChasePosition);
    }

    private void Attacking()
    {
        LookRotationTarget();
        if (nativeAttackState == AttackTypeStates.Melee)
        {

        }
        else
        {

        }
    }

    private void LookRotationTarget()
    {
        attackDirection = player.position - transform.position;
        attackDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(attackDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * settings.angularSpeed);
    }

    private void OnDrawGizmos()
    {
        DrawWireDisc(settings.colors[0], centerPoint, transform.up, settings.alphaPatrolRadius);
        DrawWireDisc(settings.colors[1], transform.position, transform.up, settings.patrolRadius);
        DrawWireDisc(settings.colors[2], transform.position, transform.up, settings.distanceAttackRadius);
        DrawWireDisc(settings.colors[3], transform.position, transform.up, settings.meleeAttackRadius);
        DrawWireDisc(settings.colors[4], transform.position, transform.up, settings.searchRadius);
        DrawWireDisc(colorsFov[0], transform.position, transform.up, settings.detectionRadius);
        UnityEditor.Handles.color = colorsFov[1];
        Vector3 rotatedForward = Quaternion.Euler(0, -settings.detectionAngle * 0.5f, 0) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, settings.detectionAngle, settings.detectionRadius);
    }

    public void DrawWireDisc(Color color, Vector3 center, Vector3 normal, float radius)
    {
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawWireDisc(center, normal, radius);
    }
}

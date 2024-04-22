using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NativeEnemy : MonoBehaviour
{
    public float patrolSpeed = 3f;
    public float chaseSpeed = 6f;
    public float detectionRadius = 10f;
    public float detectionAngle = 90f;
    public float patrolRadius = 5f; // Nuevo parámetro: radio de la zona de patrulla

    private Transform player;
    private NavMeshAgent agent;
    private bool isChasing = false;
    private Vector3 spawnPoint; // Punto de origen del enemigo

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = transform.position; // Guarda el punto de origen
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!isChasing)
            {
                Vector3 randomPoint = Random.insideUnitSphere * patrolRadius;
                randomPoint += spawnPoint; // Offset para centrar el círculo alrededor del punto de origen
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
            }
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle <= detectionAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit, detectionRadius))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        isChasing = true;
                        agent.speed = chaseSpeed;
                        Vector3 behindPlayer = player.position - player.forward * 2f; // Posición detrás del jugador
                        agent.SetDestination(behindPlayer);
                    }
                }
            }
        }
        else
        {
            isChasing = false;
            agent.speed = patrolSpeed;
        }
    }
}

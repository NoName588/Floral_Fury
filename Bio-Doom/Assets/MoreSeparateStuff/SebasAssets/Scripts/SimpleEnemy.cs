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

    private Vector3 startingPoint;

    private void Awake()
    {
        state = State.Roaming;
    }

    private void Start()
    {
        startingPoint = transform.position;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                RoamingPoint();
                break;
            case State.ChaseTarget:
                break;
        }
    }

    private Vector3 RoamingPoint()
    {
        return startingPoint + GetRandomDir() * Random.Range(10f, 70f);
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    /*private void FindTarget()
    {
        float targetRange = 50f;
        if(Vector3.Distance(transform.position) < targetRange)
        {
            state = State.ChaseTarget;
        }
    }*/
}

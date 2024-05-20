using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshHandler : MonoBehaviour
{
    //[SerializeField] private Transform movePositionTarget;
    public NavMeshAgent navMeshAgent;

    public float pathFindingTarget = 5f;
    private float pathRandomPosition = 50f;

    public Transform centrePoint;

    private Transform[] randomPositionArray;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void RoamingMovement()
    {
        Debug.Log("Position to go is now running");
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Debug.Log("EnteredCondition");
            Vector3 point;
            if(RandomPoint(centrePoint.position, pathFindingTarget, out point))
            {
                Debug.Log($"Position to go is {point}");
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                navMeshAgent.SetDestination(point);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        //Sacar un arreglo de transforms para diferentes enemigos y así posicionarlos mejor sobre un espacio
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        Debug.Log("Entered calulated random");

        if(NavMesh.SamplePosition(randomPoint, out hit, 100.0f, NavMesh.AllAreas))
        {
            Debug.Log("Entered if condition");
            result = hit.position;
            return true;
        }

        Debug.Log("After if");

        result = Vector3.zero;
        return false;
    }

    public void ChasingMethod(Vector3 playerTransform)
    {
        navMeshAgent.destination = playerTransform;
    }
}

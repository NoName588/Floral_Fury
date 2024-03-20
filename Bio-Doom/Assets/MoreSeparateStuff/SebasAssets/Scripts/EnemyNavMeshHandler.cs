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
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Vector3 point;
            if(RandomPoint(centrePoint.position, pathFindingTarget, out point))
            {
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

        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void ChasingMethod(Transform playerTransform)
    {
        navMeshAgent.destination = playerTransform.position;
    }
}

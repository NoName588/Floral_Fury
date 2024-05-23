using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Data Base Enemy Settings ", order = 1)]
public class BaseEnemySettings : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 patrolStandRangeTime = new Vector2(1f, 2f);
    public List<Color> colors = new List<Color>();
    public float alphaPatrolRadius = 10f;
    public float patrolRadius = 7f;
    public float patrolSpeed = 3f;
    public float distanceAttackRadius = 6.5f;
    public float meleeAttackRadius = 2f;
    public float detectionRadius = 10f;
    public float detectionAngle = 90f;
    public float chaseSpeed = 6f;
    public float timePatrol = 5f;
    public float angularSpeed = 6f;
    public float searchRadius = 12f;

    public void Init()
    {

    }
    public void OnBeforeSerialize()
    {
        Init();
    }

    public void OnAfterDeserialize()
    {

    }
}

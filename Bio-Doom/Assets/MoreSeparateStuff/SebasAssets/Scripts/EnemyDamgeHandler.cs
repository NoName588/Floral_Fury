using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamgeHandler : MonoBehaviour
{
    [SerializeField] private GameObject attackHitbox;

    public int life = 20;
    public int enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        attackHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Enemy Life is {life}");
    }

    public void Attack()
    {
        attackHitbox.SetActive(true);
    }

    public void AttackOFF()
    {
        attackHitbox.SetActive(false);
    }
}

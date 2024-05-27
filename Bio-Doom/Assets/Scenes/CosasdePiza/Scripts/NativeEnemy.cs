using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NativeEnemy : MonoBehaviour
{
   /* Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            Enemy enemy = target.GetComponentInParent<Enemy>();
            if (!ConfirmEnemy(enemy))
                this.enemy.enemiesList.Add(enemy);
        }
    }

    bool ConfirmEnemy(Enemy enemy)
    {
        if (enemy != null) {
        
            return this.enemy.enemiesList.Contains(enemy);
        }
        return false;
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            Enemy enemy = target.GetComponentInParent<Enemy>();
            this.enemy.enemiesList.Remove(enemy);
        }
    }*/
}

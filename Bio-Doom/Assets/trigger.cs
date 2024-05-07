using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{

    public List<Enemy> enemyList = new List<Enemy>();
    public bool enable = false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            enable = true;
            Enemy enemy = target.GetComponent<Enemy>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Enemy"))
        {
            enable = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamgeHandler : MonoBehaviour
{
    [SerializeField] private BoxCollider attckCollider;

    public int lifeEnemy = 20;
    public int enemyDamage;

    public AudioSource sound;
    public AudioClip Admin;
    // Start is called before the first frame update
    void Start()
    {
        attckCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Enemy Life is {lifeEnemy}");
    }

    public void Attack()
    {
        attckCollider.enabled = true;
        sound.PlayOneShot(Admin);
    }

    public void AttackOFF()
    {
        attckCollider.enabled = false;
        sound.Pause();
    }
}

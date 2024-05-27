using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageDealt : MonoBehaviour
{
   
    [SerializeField] private int damageToEnemy;
   
    private List<EnemyDamgeHandler> enemiesListDetected = new List<EnemyDamgeHandler>();
    //private List<EnemyDamgeHandler> bossListDetected = new List<EnemyDamgeHandler>();

    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyLayer"))
        {
            EnemyDamgeHandler enemyLifeHandler = other.GetComponent<EnemyDamgeHandler>();
            if (enemyLifeHandler != null && !enemiesListDetected.Contains(enemyLifeHandler))
            {
                enemiesListDetected.Add(enemyLifeHandler);
                enemyLifeHandler.lifeEnemy -= damageToEnemy;
                source.PlayOneShot(clip);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyLayer"))
        {
            EnemyDamgeHandler enemyLifeHandler = other.GetComponent<EnemyDamgeHandler>();
            if (enemyLifeHandler != null && enemiesListDetected.Contains(enemyLifeHandler))
            {
                enemiesListDetected.Remove(enemyLifeHandler);
                source.Stop();
            }
        }
    }
}

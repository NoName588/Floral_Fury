using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageDealt : MonoBehaviour
{
    [SerializeField] private EnemyDamgeHandler enemyLifeHandler;
    [SerializeField] private int damageToEnemy;
    [SerializeField] private GameObject triggerHitboxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        triggerHitboxPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            triggerHitboxPlayer.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyLifeHandler.life -= damageToEnemy;
    }
}

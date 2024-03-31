using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxHandler : MonoBehaviour
{
    [SerializeField] private EnemyDamgeHandler enemyHandler;
    [SerializeField] private LifePlaceHolderHandler lifePlayer;
    //[SerializeField] private GameObject hitboxObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        lifePlayer.LifeChange(enemyHandler.enemyDamage);
        //hitboxObject.SetActive(false);
    }
}

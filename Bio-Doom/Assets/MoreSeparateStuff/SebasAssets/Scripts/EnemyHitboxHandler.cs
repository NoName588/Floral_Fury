using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxHandler : MonoBehaviour
{
    [SerializeField] private int damage;
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
        if (other.gameObject.CompareTag("Player"))
        {
            lifePlayer.LifeChange(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxHandler : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private LifePlaceHolderHandler lifePlayer;
    [SerializeField] private GameObject imageOfFeedback;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            imageOfFeedback.SetActive(true);
            lifePlayer.LifeChange(damage);
        }
    }
}

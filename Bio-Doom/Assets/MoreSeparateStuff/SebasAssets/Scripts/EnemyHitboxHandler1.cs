using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxHandler1 : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private LifePlaceHolderHandler lifePlayer;
    [SerializeField] private GameObject imageOfFeedback;


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
            CShake.instance.ShakeCam(5, 5, 1f);
            imageOfFeedback.SetActive(true);
            lifePlayer.LifeChange(damage);
        }
    }
}

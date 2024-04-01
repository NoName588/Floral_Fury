using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlaceHolderHandler : MonoBehaviour
{
    public int life = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 0)
        {
            Debug.Log("Current Life Ended");
        }
        else if(life > 0)
        {
            Debug.Log($"Current Life is {life}");
        }
    }

    public void LifeChange(int dmgDealt)
    {
        life -= dmgDealt;
    }
}

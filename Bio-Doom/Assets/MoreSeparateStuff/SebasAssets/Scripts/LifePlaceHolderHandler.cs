using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlaceHolderHandler : MonoBehaviour
{
    public int life = 20;
    [SerializeField] private GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 0)
        {
            gameOverCanvas.SetActive(true);
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

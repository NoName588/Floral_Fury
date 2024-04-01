using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaJuego : MonoBehaviour
{ 
    public bool juegoPausado = false;
    public GameObject menuPausa;

    public void Start()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }

        void PausarJuego()
        {
            juegoPausado = true;
            menuPausa.SetActive(true);
            Time.timeScale = 0f;
        }

        void ReanudarJuego()
        {
            juegoPausado = false;
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void Salirjuego()
    {
        juegoPausado = false;
        menuPausa.SetActive(false);
        Time.timeScale = 2f;
    }
}
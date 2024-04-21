using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cerrarjuego : MonoBehaviour
{


    public void EmpezarNivel(string Nombrenivel)
    {
      SceneManager.LoadScene(NombreNivel);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("se cierra el juego");
    }

}

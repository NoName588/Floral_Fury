using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cerrarjuego : MonoBehaviour
{
    public void EmpezarNivel(string Nombrenivel)
    {
        SceneManager.LoadScene(Nombrenivel);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("se cierra el juego");
    }

}

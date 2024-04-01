using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    public void MenuInicial(string nombre)
    {
         SceneManager.LoadScene(nombre);
    }

    public void MenuSalir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}



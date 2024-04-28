using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("EmmaTestScene");
    }
    public void Quitgame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("EscenaMenu");
    }
}

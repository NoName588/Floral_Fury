using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
}
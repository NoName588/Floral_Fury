using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackMainMenu : MonoBehaviour
{

    public void GoBackMain()
    {
        SceneManager.LoadScene("Menuprincipal");
    }
}

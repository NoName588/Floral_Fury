using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loeader : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("SebasTestScene3", LoadSceneMode.Single);
    }
}

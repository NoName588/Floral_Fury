using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    public string scenename;
    public void LoadScene()
    {
        SceneManager.LoadScene(scenename);
    }
}

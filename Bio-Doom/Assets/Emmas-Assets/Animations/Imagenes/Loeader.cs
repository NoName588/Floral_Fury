using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loeader : MonoBehaviour
{
    public GameObject obj;
    private void Start()
    {
        obj.SetActive(false);
    }
    private void Update()
    {
        if(obj.activeSelf == true)
        {
            SZS();
        }
    }

    private void SZS()
    {
        SceneManager.LoadScene("EmmaTestScene", LoadSceneMode.Single);
    }
}

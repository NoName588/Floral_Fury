using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Salida : MonoBehaviour
{
    // Start is called before the first frame update
    void start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SalidaButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif 

    }
}

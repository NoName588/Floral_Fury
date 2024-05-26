using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActivate : MonoBehaviour
{
    // Tiempo en segundos despu�s del cual se desactivar� el GameObject
    private float tiempoDesactivacion = 7.0f;

    void Start()
    {
        // Inicia una corrutina para desactivar el GameObject despu�s del tiempo especificado
        StartCoroutine(DesactivarGameObjectDespuesDeTiempo());
    }

    IEnumerator DesactivarGameObjectDespuesDeTiempo()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(tiempoDesactivacion);

        // Desactiva el GameObject
        gameObject.SetActive(false);
    }
}


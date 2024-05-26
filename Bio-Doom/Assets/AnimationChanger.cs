using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationChanfer : MonoBehaviour
{
    // Tiempo en segundos después del cual se desactivará el GameObject y se cambiará la escena
    public float tiempoEspera = 80.0f; // 1 minuto y 20 segundos = 80 segundos

    // Nombre de la escena a la que se cambiará
    public string nombreEscena = "MiNuevaEscena"; // Reemplaza con el nombre real de tu escena

    void Start()
    {
        // Inicia una corrutina para desactivar el GameObject y cambiar la escena después del tiempo especificado
        StartCoroutine(DesactivarGameObjectYCambiarEscenaDespuesDeTiempo());
    }

    IEnumerator DesactivarGameObjectYCambiarEscenaDespuesDeTiempo()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(tiempoEspera);

        // Desactiva el GameObject
        gameObject.SetActive(false);

        // Carga la nueva escena
        SceneManager.LoadScene(nombreEscena);
    }
}


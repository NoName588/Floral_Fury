using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public Transform jugador; 
    public float distanciaDeteccion = 10f; 
    public float distanciaAtaqueDistancia = 5f; 
    public float distanciaAtaqueMele = 2f; 
    public float velocidadMovimiento = 2f; 
    public float tiempoDisparo = 1f; 
    public GameObject proyectil; 

    private bool persiguiendo = false; 
    private bool atacandoDistancia = false; 
    private bool atacandoMele = false; 
    private float tiempoUltimoDisparo = 0f; 

    void Start()
    {
        // Inicializar variables
        persiguiendo = false;
        atacandoDistancia = false;
        atacandoMele = false;
        tiempoUltimoDisparo = 0f;
    }

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidadMovimiento * Time.deltaTime);

        
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        
        if (distanciaAlJugador <= distanciaDeteccion)
            persiguiendo = true;
        else
            persiguiendo = false;

        
        if (persiguiendo)
        {
            
            if (Vector3.Dot(transform.forward, jugador.position - transform.position) > 0)
            {
               
                transform.position += transform.forward * velocidadMovimiento * Time.deltaTime;
                atacandoDistancia = false;
            }
            else
            {
                
                transform.position -= transform.forward * velocidadMovimiento * Time.deltaTime;

                
                if (distanciaAlJugador <= distanciaAtaqueDistancia)
                {
                    atacandoDistancia = true;
                    atacandoMele = false;
                }
            
                else if (distanciaAlJugador <= distanciaAtaqueMele)
                {
                    atacandoDistancia = false;
                    atacandoMele = true;
                }
                else
                {
                    atacandoDistancia = false;
                    atacandoMele = false;
                }
            }

            
            if (atacandoDistancia && Time.time >= tiempoUltimoDisparo + tiempoDisparo)
            {
                tiempoUltimoDisparo = Time.time;
                Instantiate(proyectil, transform.position, transform.rotation);
            }

            if (atacandoMele)
            {
               
            }
        }
    }
}


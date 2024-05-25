using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMOVE : MonoBehaviour
{
    public Animator animator_opciones;
    public GameObject Opciones_first_selected;
    public GameObject principal_first;
    // Start is called before the first frame update
    public void MostrarOpciones() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Opciones_first_selected);
        animator_opciones.SetTrigger("Open");

        
    }

    // Update is called once per frame
     public void CerrarOpciones()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(Opciones_first_selected);
        animator_opciones.SetTrigger("open");
    }
}

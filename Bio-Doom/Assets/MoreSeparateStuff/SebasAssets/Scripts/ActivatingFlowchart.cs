using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ActivatingFlowchart : MonoBehaviour
{
    [SerializeField]
    private Flowchart flowToActivate;

    [SerializeField]
    private string blockName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            flowToActivate.gameObject.SetActive(true);
            flowToActivate.ExecuteBlock(blockName);
        }
    }
}

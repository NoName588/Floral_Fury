using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthShow : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private BossHealthHandler enemyScript;
    [SerializeField] private GameObject cameraFollow;

    private TextMeshPro textForHealth;
    private int enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        textForHealth = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraFollow.transform);
        Vector3 directionToCamera = cameraFollow.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(-directionToCamera);
        enemyHealth = enemyScript.healthBoss;
        textForHealth.text = enemyHealth.ToString();

        if (enemyScript.healthBoss <= 0)
        {
            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        Destroy(enemyObject);
    }
}

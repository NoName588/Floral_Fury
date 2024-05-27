using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class OilHealthShow : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private EnemyDamgeHandler enemyScript;
    [SerializeField] private GameObject cameraFollow;
    [SerializeField] private ChangeObjectiveEnemyCount countOfObjective;

    public AudioSource Audi;
    public AudioClip Sonido;
    public GameObject win;

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
        enemyHealth = enemyScript.lifeEnemy;
        textForHealth.text = enemyHealth.ToString();

        if (enemyScript.lifeEnemy <= 0)
        {
            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        countOfObjective.countObjectiveEnemy += 1;

        win.SetActive(true);
        Audi.PlayOneShot(Sonido);
        Destroy(enemyObject);

        Debug.Log("nivel ganado");
    }
}


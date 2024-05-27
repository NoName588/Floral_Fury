using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class EnemyHealthShow : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private EnemyDamgeHandler enemyScript;
    [SerializeField] private GameObject cameraFollow;
    [SerializeField] private ChangeObjectiveEnemyCount countOfObjective;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private NavMeshAgent enemyPathFinding;
    [SerializeField] private GameObject objectOfShow;
    [SerializeField] private MeleeEnemy enemyMainMove;

    public AudioSource Audi;
    public AudioClip Sonido;

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

        if(enemyScript.lifeEnemy <= 0)
        {
            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        countOfObjective.countObjectiveEnemy += 1;
        //Destroy(enemyObject);
        enemyAnimator.SetTrigger("Dead");
        enemyPathFinding.isStopped = true;
        objectOfShow.SetActive(false);
        enemyPathFinding.enabled = false;
        Audi.PlayOneShot(Sonido);
    }
}

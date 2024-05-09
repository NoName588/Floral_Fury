using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeObjectiveEnemyCount : MonoBehaviour
{
    private TMP_Text textTMP;
    public int countObjectiveEnemy = 0;

    [SerializeField] private int countToGetObjective;
    [SerializeField] private string sceneToLoad;

    private void Awake()
    {
        textTMP = gameObject.GetComponent<TMP_Text>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textTMP.text = countObjectiveEnemy.ToString();

        if(countObjectiveEnemy == countToGetObjective)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

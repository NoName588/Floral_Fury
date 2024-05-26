using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransScene : MonoBehaviour
{
    public string loader;

    public int wait = 3;

    public Animator animator;

    public void Start()
    {
        StartCoroutine(LoadLevel());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel();
        }
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene(loader);
    }
}

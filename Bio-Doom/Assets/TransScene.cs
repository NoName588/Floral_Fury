using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransScene : MonoBehaviour
{
    public Animator animator;
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

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("escenario_2");
    }
}

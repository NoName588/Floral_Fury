using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransScene : MonoBehaviour
{
    public Animator animator;
    public void LoadScene()
    {
        animator.SetTrigger("Start");
    }
}

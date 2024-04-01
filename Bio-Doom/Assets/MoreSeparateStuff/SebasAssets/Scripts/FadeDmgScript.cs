using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeDmgScript : MonoBehaviour
{
    private float alpha = 0f;
    private float fadeSpeed = 0.5f;
    private Image imageUi;

    private void Start()
    {
        imageUi = GetComponent<Image>();
    }

    private void Update()
    {
        alpha += fadeSpeed * Time.deltaTime;
        imageUi.color = new Color(1f, 1f, 1f, alpha);

        if (alpha >= 1f || alpha <= 0f)
        {
            fadeSpeed = -fadeSpeed;
            gameObject.SetActive(false);
        }
    }
}

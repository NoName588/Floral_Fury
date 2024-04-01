using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthShow : MonoBehaviour
{
    [SerializeField] private LifePlaceHolderHandler playerLife;

    private TextMeshProUGUI textMeshUi;
    private int healthPlayer;

    // Start is called before the first frame update
    void Start()
    {
        textMeshUi = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthPlayer = playerLife.life;
        textMeshUi.text = healthPlayer.ToString();
    }
}

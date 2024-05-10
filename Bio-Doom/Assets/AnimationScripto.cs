using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScripto : MonoBehaviour
{
    public GameObject Sword_L;
    public GameObject Sword_R;

    public void SerActiveSword()
    {
        Sword_L.SetActive(true);
        Sword_R.SetActive(true);
    }

    public void SetFalse()
    {
        Sword_L.SetActive(false);
        Sword_R.SetActive(false);
    }
}

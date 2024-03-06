using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, rn_speed, ro_speed;
    public bool walking;
    public Transform playerTrans;


    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W)) 
        {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.S)) 
        { 
            playerRigid.velocity = -transform.forward * rn_speed * Time.deltaTime; 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            playerAnim.SetTrigger("Slow Run");
            playerAnim.ResetTrigger("Idle");
            walking = true; 
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("Slow Run");
            playerAnim.ResetTrigger("Idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("Slow Run");
            playerAnim.ResetTrigger("Idle");
            walking = true;
        }

    }

    void Start()
    {

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class kPlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D triangleP;

    [SerializeField] int velocity;

    [SerializeField] int jumpPower;

    [SerializeField] BoxCollider2D foot;

    bool onGround;

    int jumpCounter;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            triangleP.GetComponent<SpriteRenderer>().flipX = true;
            triangleP.AddForce(Vector2.right * Time.deltaTime * velocity);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            triangleP.GetComponent<SpriteRenderer>().flipX = false;
            triangleP.AddForce(Vector2.left * Time.deltaTime * velocity);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (onGround)
            {
                triangleP.AddForce(Vector2.up * jumpPower);
                jumpCounter++;
            }
            else
            {
                if(jumpCounter == 1)
                {
                    triangleP.AddForce(Vector2.up * jumpPower);
                    jumpCounter = 2;
                }
                else
                {
                    jumpCounter %= 2;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}

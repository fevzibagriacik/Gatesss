using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class kPlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D triangleP;

    [SerializeField] int velocity;

    [SerializeField] float jumpPower;

    [SerializeField] PhysicsMaterial2D friction;

    bool keyPressed = false;

    bool onGround;

    int jumpCounter = 0;

    void Start()
    {
        onGround = true;
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
        if (Input.GetKeyUp(KeyCode.LeftArrow) || (Input.GetKeyUp(KeyCode.RightArrow))) 
        {
            Vector2 velocity = triangleP.velocity;
            velocity.x = 0;
            triangleP.velocity = velocity;
        }
        
        //if (!keyPressed) {  }
        //HorizontalMove();
       
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("UP");
            if (onGround || jumpCounter < 2)
            {
                triangleP.AddForce((Vector2.up) * jumpPower, ForceMode2D.Impulse);
                jumpCounter++;
                onGround = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            jumpCounter = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    void HorizontalMove()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1;
            triangleP.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            triangleP.GetComponent<SpriteRenderer>().flipX = true;
            dir.x = 1;
        }

        dir.Normalize();

        triangleP.velocity = velocity * dir;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kPlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D triangleP;

    [SerializeField] int velocity;

    [SerializeField] int jumpPower;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            triangleP.AddForce(Vector2.right * Time.deltaTime * velocity);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            triangleP.AddForce(Vector2.left * Time.deltaTime * velocity);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            triangleP.AddForce(Vector2.up * jumpPower);
        }
    }
}

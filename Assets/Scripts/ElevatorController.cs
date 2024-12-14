using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    int direction;
    void Start()
    {
        direction = 1;
    }
    void Update()
    {
        rb.velocity = Vector2.up * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            direction *= -1;
        }
    }


}

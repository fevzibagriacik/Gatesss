using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private Vector3 spawnPoint;
    private void Start()
    {
        spawnPoint = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("pakdoaskdpokas");
        }
        else
        {
            transform.position = spawnPoint;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("TargetSpawnPoint"))
        {
            return;
        }
    }
}

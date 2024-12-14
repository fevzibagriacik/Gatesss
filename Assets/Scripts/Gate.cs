using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] GameObject popUp;
    BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        popUp.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        popUp.SetActive(false);
    }
}

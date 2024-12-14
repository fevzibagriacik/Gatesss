using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitControl : MonoBehaviour
{
    [SerializeField] GameObject eggPre;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Egg"))
        {
            Destroy(collision.gameObject);
            Instantiate(eggPre);
        }
    }
}

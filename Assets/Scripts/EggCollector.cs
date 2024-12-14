using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollector : MonoBehaviour
{
    [SerializeField] GameObject pl1;
    [SerializeField] GameObject egg;
    void Start()
    {
        pl1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Egg"))
        {
            pl1.SetActive(true);
            Destroy(egg);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    [SerializeField] GameObject eggPre;
    void Start()
    {
        Instantiate(eggPre);
        eggPre.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (eggPre.transform.position.y < -9) 
        { 
            Destroy(eggPre);
            Instantiate(eggPre);
            eggPre.transform.position = this.transform.position;
        }
    }
}

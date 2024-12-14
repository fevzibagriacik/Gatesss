using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate3D : MonoBehaviour
{
    public Vector3 originalPos;
    bool moveBack;

    // Update is called once per frame
    private void Start()
    {
        originalPos= transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player3D")
        {
            GetComponent<MeshRenderer>().material.color = Color.green; 
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player3D")
        {

        }
        if (collision.gameObject.tag == "parentCube")
        {

        }
    }
}

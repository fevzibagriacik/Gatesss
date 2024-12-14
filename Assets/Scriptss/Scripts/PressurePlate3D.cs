using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate3D : MonoBehaviour
{
    public Vector3 originalPos;
   
    public float pressurePlateSpeed=0.01f;
    bool notOnFloor=true;
    //bool canGoBack=false;
    bool goneBack=true;
    bool somethingAtTop = false;
    [SerializeField] GateController Gate_controller;

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
        if (collision.gameObject.tag == "parentCube")
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player3D"&&notOnFloor)
        {
            transform.Translate(0f,-pressurePlateSpeed,0f);
            goneBack = false;
            somethingAtTop = true;
        }
        if (collision.gameObject.tag == "parentCube" && notOnFloor)
        {
            transform.Translate(0f, -pressurePlateSpeed, 0f);
            goneBack = false;
            somethingAtTop = true;
        }
    }
    private void Update()
    {
        
        if(Mathf.Abs(gameObject.transform.position.y-originalPos.y)>= 0.1)
        {
            notOnFloor = false;
            Gate_controller.GateOpen = true;

        }
        if (Mathf.Approximately(originalPos.y- transform.position.y,0f))
        {
            goneBack = true;
        }
        if (!somethingAtTop && !goneBack && transform.position.y < originalPos.y)
        {
            transform.Translate(0f, +pressurePlateSpeed, 0f);
            notOnFloor = true;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player3D"&&goneBack==false )
        {
            somethingAtTop = false;
            goneBack=false;
            GetComponent<MeshRenderer>().material.color = Color.red;
            Gate_controller.GateOpen = false;
        }
        if (collision.gameObject.tag == "parentCube"&&goneBack==false)
        {
            goneBack=false;
            somethingAtTop = false;
            GetComponent<MeshRenderer>().material.color = Color.red;
            Gate_controller.GateOpen = false;
        }
    }

}

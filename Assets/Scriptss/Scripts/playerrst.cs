using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerrst : MonoBehaviour
{
    // Start is called before the first frame update
    private bool CubetriggerEntered = false;
    private bool CubeCanBeCatched = false;
    private bool catched = false;
    public float holdLift;
    public float holdLength;
    //public Camera camera;
    public float catchingRadius=0.5f;
    public float catchLength;
    RaycastHit hitt;
    Vector3 catching;
    public float cameraSensivity;
    float rotationX = 0f;
    float rotationY = 0f;
    //public float catchSize;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        catching= new Vector3(0f, 0f, catchLength);
        CubeControl();
        if (catched)
        {
            CatchingHolding();
        }

        rotationX += Input.GetAxis("Mouse X");
        rotationY = Input.GetAxis("Mouse Y");

    }





    private void CubeControl()
    {
        RaycastHit hit;
        Ray ray;
        if (CubetriggerEntered && catched == false)
        {
            Debug.Log("sensedCube");
            
            
            ray = new Ray(gameObject.transform.position, gameObject.transform.position + catching);
            if (Physics.BoxCast(gameObject.transform.position,new Vector3(0.3f,0.3f,0.3f),new Vector3(0, 0, 1), out hit))
            {
                if (hit.collider.tag=="cube")
                {
                    CubeCanBeCatched = true;
                    //hit = null;
                }
                
            }

        }

        if (CubeCanBeCatched)
        {
            Debug.Log("CubeCanBeCatched");
            if (Input.GetKey(KeyCode.E))
            {
                ray = new Ray(gameObject.transform.position, gameObject.transform.position + catching);
                if (Physics.BoxCast(gameObject.transform.position, new Vector3(0.3f, 0.3f, 0.3f), new Vector3(0, 0, 1), out hit))
                {
                    catched = true;
                    if (hit.collider.GetComponentInParent<Transform>().gameObject.CompareTag("parentCube")){
                        hit.collider.GetComponentInParent<Transform>().position = transform.position + new Vector3(0f, holdLift,holdLength );
                        hitt = hit;   
                    }
                }
                
            }
        }
    }
    private void CatchingHolding()
    {
        
        Ray ray;
        if (Input.GetKey(KeyCode.E))
        {
            ray = new Ray(gameObject.transform.position, gameObject.transform.position + catching*2);
            
            
                catched = true;
                if (hitt.collider.GetComponentInParent<Transform>().gameObject.CompareTag("parentCube"))
                {
                    Debug.Log("parentdetect");
                    hitt.collider.GetComponentInParent<Transform>().position = transform.position + new Vector3(0f, holdLift, holdLength);
                    

                }
            }
        
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (hitt.collider.GetComponentInParent<Transform>().gameObject.CompareTag("parentCube"))
            {
                hitt.collider.GetComponentInParent<Transform>().eulerAngles = new Vector3(0f, 0f, 0f);

            }
            
            catched = false;
        }
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cube")
        {
            CubetriggerEntered=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CubetriggerEntered=false;
        CubeCanBeCatched=false;
        
        Debug.Log("Exited");
        
    }
}

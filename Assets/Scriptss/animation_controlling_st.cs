using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_controlling_st : MonoBehaviour
{
    public Animator animator;
    public GameObject camera;

    // Update is called once per frame
    void Update()
    {
        if(camera.transform.position.x== 2.06f)
        {
            animator.SetBool("SlimesMove", true);
        }
    }
}

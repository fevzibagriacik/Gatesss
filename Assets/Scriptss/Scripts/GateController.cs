using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public bool GateOpen;
    [SerializeField] Animator animator;

    private void Update()
    {
        if (GateOpen)
        {

            animator.SetBool("Openning", true);
            animator.SetBool("OpenCancelled", false);

        }
        if (!GateOpen)
        {
            animator.SetBool("Openning", false);
            animator.SetBool("OpenCancelled",true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [SerializeField] GameObject popUp;
    BoxCollider2D col;
    public bool GatePressed;
    private bool E_check = false;


    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (E_check)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        popUp.SetActive(true);
        E_check = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        popUp.SetActive(false);
        E_check = false;
    }
}
